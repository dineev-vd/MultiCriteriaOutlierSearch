import codecs
import json

from flask import request, jsonify, Flask
from pyod.models.combination import moa
import numpy as np

app = Flask(__name__)


@app.route('/combinations/moa/', methods=['POST'])
def majority_get():
    try:
        array = np.array(request.json["Data"])
        weights = [1 for i in array]
        n_buckets = 1

        if "n_buckets" in request.json:
            n_buckets = np.array([request.json['Weights']])

        result = moa(np.transpose(array), n_buckets=n_buckets)
        return jsonify({"data":result.tolist(),"message":"OK"})
    except Exception as e:
        return jsonify({"message":str(e)}), 400

@app.route('/combinations/moa/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)