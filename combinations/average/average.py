import codecs
import json
from flask import request, jsonify, Flask
from pyod.models.combination import average
import numpy as np

app = Flask(__name__)


@app.route('/combinations/average/', methods=['POST'])
def majority_get():
    try:
        data = request.json['Data']
        array = np.array(data['values'])
        weights = np.array([1 for i in array])

        if "weights" in data:
            weights = np.array([data['weights']])

        result = average(np.transpose(array), weights)
        return jsonify({"data":result.tolist(),"message":"OK"})
    except Exception as e:
        return jsonify({"message":str(e)}), 400

@app.route('/combinations/average/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)

if __name__ == '__main__':
    app.run(debug=True, port=7000)