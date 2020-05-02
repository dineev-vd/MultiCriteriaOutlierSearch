import codecs
import json

from flask import Flask, request, jsonify
import numpy as np


app = Flask(__name__)

@app.route('/<path:path>', methods=['POST'])
def threesigma(path):
    try:
        data = np.array(request.json["Data"])
        if data.shape != (len(data), 1):
            raise Exception

        data = data.reshape(1, -1).flatten()
        print(data)
        sigma = np.std(data)
        mean = np.mean(data)
        print(mean, sigma)
        indices = []
        for i in range(len(data)):
            if not(mean - 3*sigma <= data[i] and data[i] <= mean + 3*sigma):
                indices.append(i)
        a = [0 for i in range(0,len(data))]
        for i in indices:
            a[i] = 1
    except Exception as e:
        return jsonify({"message":str(e)}), 400
    return jsonify({"data":a,"message":"OK"})

@app.route('/algorithms/threesigma/config/', methods=['GET'])
def config():
    with codecs.open("config.json",encoding='utf8') as json_file:
        data = json.load(json_file)
    return jsonify(data)