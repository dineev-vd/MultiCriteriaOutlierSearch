import codecs
import json

from flask import request, jsonify, Flask
from pyod.models.combination import average
import numpy as np

app = Flask(__name__)


@app.route('/<path:path>', methods=['POST'])
def majority_get(path):
    try:
        array = np.array(request.json['Data']['values'])
        weights = np.array([request.json['Data']['weights']])
        result = average(np.transpose(array), weights)
        return jsonify({"data":result.tolist(),"message":"OK"})
    except Exception as e:
        return jsonify({"message":str(e)}), 400

@app.route('/combination/average/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)

if __name__ == '__main__':
    app.run(debug=True, port=7000)