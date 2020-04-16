import os
import uuid
import jsonpickle as jp

from flask import Flask, request, render_template, url_for
from flask_cors import CORS, cross_origin

from api.settings.settings import IS_IN_DEBUG_MODE, API_PORT, API_SECRET, DEPLOY_LINK
from api.models.dataset import DatasetHeader, DatasetToRead, RecordDTO
from api.models.analytics import Polynomial, AccuracyEstimations

from dal.repositories.repository import DatasetRepository
from dal.models.dataset import Dataset

app = Flask(__name__)
app.secret_key = API_SECRET

CORS(app, resources={r"/api/*": {"origins": DEPLOY_LINK}})
app.config['CORS_HEADERS'] = 'Content-Type'

dataset_repository = DatasetRepository()


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

        records = []

        for record in obj["records"]:
            records = RecordDTO(record["inputs"], record["output"])

        dataset_repository.insert_new_dataset(Dataset(str(uuid.uuid4()), obj["name"]))
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
        return "Dataset updated successfully!"
    elif request.method == 'DELETE':
        dataset_repository.delete_dataset(guid)
        return "Dataset deleted successfully!"
    else:
        outputs_only_val = none_query_helper(request.args.get('outputsOnly'), False)

        dataset = dataset_repository.get_dataset_by_id(guid)
        mock = DatasetToRead(dataset.Id, dataset.Name, [RecordDTO([0.0, 0.0], 0)])
        return str(jp.encode(mock, unpicklable=False))


@app.route('/api/analysis/<guid>/equation', methods=['GET'])
@cross_origin()
def get_dataset_equation(guid):
    digits_val = none_query_helper(request.args.get('digits'), 3)
    mock = Polynomial([8.94, -0.345, 0.013, -0.043])
    return str(jp.encode(mock, unpicklable=False))


@app.route('/api/analysis/<guid>/estimations', methods=['GET'])
@cross_origin()
def get_dataset_estimations(guid):
    digits_val = none_query_helper(request.args.get('digits'), 3)
    mock = AccuracyEstimations([3.864, -0.638, -2.2, 0.311, 2.639, 6.05, 7.031, 1.456, 0.468],
                               [2.88, -0.521, -0.903, 1.223, 5.013, 5.915, 7.751, -0.622, -1.753], 5.632, 0.894)
    return str(jp.encode(mock, unpicklable=False))


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
