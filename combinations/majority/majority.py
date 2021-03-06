import codecs
import json

from flask import request, jsonify, Flask
from pyod.models.combination import majority_vote
import numpy as np

app = Flask(__name__)


@app.route('/combinations/majority/', methods=['POST'])
def majority_get():
    try:
        data = request.json["Data"]
        array = np.array(data['values'])
        weights = [1 for i in array]

        if "weights" in data:
            weights = np.array([data['weights']])

        result = majority_vote(np.transpose(array), weights)
        return jsonify({"data":result.tolist(),"message":"OK"})
    except Exception as e:
        return jsonify({"message":str(e)}), 400

@app.route('/combinations/majority/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)