import os
import uuid
import numpy as np
import jsonpickle as jp

from flask import Flask, request, render_template, url_for
from flask_cors import CORS, cross_origin

from sklearn.linear_model import LinearRegression

from api.settings.settings import IS_IN_DEBUG_MODE, API_PORT, API_SECRET, DEPLOY_LINK
from api.models.dataset import DatasetHeader, DatasetToRead, RecordDTO
from api.models.analytics import Polynomial, AccuracyEstimations

from dal.repositories.repository import DatasetRepository, RecordRepository
from dal.repositories.unit_of_work import UnitOfWork
from dal.models.dataset import Dataset, Record

app = Flask(__name__)
app.secret_key = API_SECRET

CORS(app, resources={r"/api/*": {"origins": DEPLOY_LINK}})
app.config['CORS_HEADERS'] = 'Content-Type'

dataset_repository = DatasetRepository()
record_repository = RecordRepository()
unit_of_work = UnitOfWork()


def none_query_helper(_input, none_to_val):
    if _input is not None:
        return _input
    else:
        return none_to_val


@app.route('/', methods=['GET'])
def index():
    return render_template('index.html')


@app.route('/<guid>', methods=['GET'])
def guid_ui(guid):
    return render_template('index.html')


@app.route('/<guid>/analytics', methods=['GET'])
def guid_analytics(guid):
    return render_template('index.html')


@app.route('/<guid>/plot', methods=['GET'])
def guid_plot(guid):
    return render_template('index.html')


@app.route('/api/dataset', methods=['GET', 'POST'])
@cross_origin()
def list_datasets():
    if request.method == 'POST':
        obj = jp.decode(str(request.json).replace("'", '"'))
        dataset_id = str(uuid.uuid4())
        dataset_repository.insert_new_dataset(Dataset(dataset_id, obj["name"]))

        for record in obj["records"]:
            record_repository.insert_record(Record(
                '|'.join([str(x) for x in record["inputs"]]),
                float(record["output"]),
                dataset_id
            ))

        unit_of_work.commit()

        return "Dataset stored successfully!"
    else:
        limit_val = none_query_helper(request.args.get('limit'), 25)
        offset_val = none_query_helper(request.args.get('offset'), 0)
        headers_only_val = none_query_helper(request.args.get('headersOnly'), False)

        datasets_db = dataset_repository.get_all()

        dataset_dto = []

        if headers_only_val:
            for item in datasets_db:
                dataset_dto.append(DatasetHeader(str(item.Id), str(item.Name)))

            return str(jp.encode(dataset_dto, unpicklable=False))
        else:
            for item in datasets_db:
                dataset_dto.append(DatasetToRead(str(item.Id), str(item.Name), [RecordDTO([0.0, 0.0], 0)]))

            return str(jp.encode(dataset_dto, unpicklable=False))


@app.route('/api/dataset/<guid>', methods=['GET', 'PUT', 'DELETE'])
@cross_origin()
def get_dataset_by_id(guid):
    if request.method == 'PUT':
        record_repository.delete_records_of_dataset(guid)
        dataset_repository.delete_dataset(guid)

        obj = jp.decode(str(request.json).replace("'", '"'))
        dataset_id = guid
        dataset_repository.insert_new_dataset(Dataset(dataset_id, obj["name"]))

        for record in obj["records"]:
            record_repository.insert_record(Record(
                '|'.join([str(x) for x in record["inputs"]]),
                float(record["output"]),
                dataset_id
            ))

        unit_of_work.commit()
        return "Dataset updated successfully!"
    elif request.method == 'DELETE':
        record_repository.delete_records_of_dataset(guid)
        dataset_repository.delete_dataset(guid)
        unit_of_work.commit()
        return "Dataset deleted successfully!"
    else:
        outputs_only_val = none_query_helper(request.args.get('outputsOnly'), False)
        dataset = dataset_repository.get_dataset_by_id(guid)
        records = record_repository.get_dataset_records(guid)

        records_dto = []

        for item in records:
            records_dto.append(RecordDTO([float(arg) for arg in str(item[1]).split('|')], float(item[2])))

        mock = DatasetToRead(dataset.Id, dataset.Name, records_dto)
        return str(jp.encode(mock, unpicklable=False))


@app.route('/api/analysis/<guid>/equation', methods=['GET'])
@cross_origin()
def get_dataset_equation(guid):
    digits_val = int(none_query_helper(request.args.get('digits'), 3))
    records = record_repository.get_dataset_records(guid)

    records_dto = []

    for item in records:
        records_dto.append(RecordDTO([float(arg) for arg in str(item[1]).split('|')], float(item[2])))

    X = np.array([record.inputs for record in records_dto])
    Y = np.array([record.output for record in records_dto])

    reg = LinearRegression().fit(X, Y)
    val = Polynomial(np.concatenate([np.array([reg.intercept_]), np.array(reg.coef_)]).tolist())
    val.koeficients = np.array([round(x, digits_val) for x in val.koeficients]).tolist()
    return str(jp.encode(val, unpicklable=False))


@app.route('/api/analysis/<guid>/estimations', methods=['GET'])
@cross_origin()
def get_dataset_estimations(guid):
    digits_val = int(none_query_helper(request.args.get('digits'), 3))
    records = record_repository.get_dataset_records(guid)
    records_dto = []

    for item in records:
        records_dto.append(RecordDTO([float(arg) for arg in str(item[1]).split('|')], float(item[2])))

    X = np.array([record.inputs for record in records_dto])
    Y = np.array([record.output for record in records_dto])

    reg = LinearRegression().fit(X, Y)
    val = Polynomial(np.concatenate([np.array([reg.intercept_]), np.array(reg.coef_)]).tolist())
    val.koeficients = np.array([round(x, digits_val) for x in val.koeficients]).tolist()

    approx_val = [float(reg.predict([record.inputs])[0]) for record in records_dto]
    disc_val = Y.tolist()
    err = round(float(sum([abs(approx_val[i] - disc_val[i]) for i in range(len(approx_val))])), digits_val)
    estimations = AccuracyEstimations(approx_val, disc_val, err, round(float(reg.score(X, Y)), digits_val))
    return str(jp.encode(estimations, unpicklable=False))


@app.route('/api/fileUpload', methods=['POST'])
@cross_origin()
def upload_file():
    target = os.path.join(os.curdir, 'static/img/')
    if not os.path.isdir(target):
        os.makedirs(target)
    file = request.files['file']
    return file.read().decode('utf-8')


if __name__ == '__main__':
    app.debug = IS_IN_DEBUG_MODE
    app.run(port=API_PORT)
