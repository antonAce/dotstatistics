import os
import jsonpickle as jp

from flask import Flask, request, render_template, url_for
from flask_cors import CORS, cross_origin

from api.settings.settings import IS_IN_DEBUG_MODE, API_PORT, API_SECRET, DEPLOY_LINK
from api.models.dataset import DatasetHeader, DatasetToRead, Record
from api.models.analytics import Polynomial, AccuracyEstimations

app = Flask(__name__)
app.secret_key = API_SECRET

CORS(app, resources={r"/api/*": {"origins": DEPLOY_LINK}})
app.config['CORS_HEADERS'] = 'Content-Type'


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
        return "Dataset stored successfully!"
    else:
        limit_val = none_query_helper(request.args.get('limit'), 25)
        offset_val = none_query_helper(request.args.get('offset'), 0)
        headers_only_val = none_query_helper(request.args.get('headersOnly'), False)

        if headers_only_val:
            mock = [DatasetHeader("5f4cbe15-326e-4c4a-a450-db18de495fd0", "test-import"),
                    DatasetHeader("4c7c6b7d-0dd1-4748-920e-8b951fdcbe1f", "2019-Finantial-Support")]
        else:
            mock = [DatasetToRead("5f4cbe15-326e-4c4a-a450-db18de495fd0", "test-import", [Record([0.0, 0.0], 0)])]

        return str(jp.encode(mock, unpicklable=False))


@app.route('/api/dataset/<guid>', methods=['GET', 'PUT', 'DELETE'])
@cross_origin()
def get_dataset_by_id(guid):
    if request.method == 'PUT':
        return "Dataset updated successfully!"
    elif request.method == 'DELETE':
        return "Dataset deleted successfully!"
    else:
        outputs_only_val = none_query_helper(request.args.get('outputsOnly'), False)
        mock = DatasetToRead(guid, "test-import", [Record([0.0, 0.0], 0)])
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
