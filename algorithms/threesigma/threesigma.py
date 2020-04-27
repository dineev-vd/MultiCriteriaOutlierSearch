from flask import Flask, request, jsonify
import numpy


app = Flask(__name__)

@app.route('/<path:path>', methods=['POST'])
def threesigma(path):
    try:
        data = request.json['Data']
        print(data)
        sigma = numpy.std(data)
        mean = numpy.mean(data)
        print(mean, sigma)
        indices = []
        for i in range(len(data)):
            if not(mean - 3*sigma <= data[i] and data[i] <= mean + 3*sigma):
                indices.append(i)
        a = [0 for i in range(0,len(data))]
        for i in indices:
            a[i] = 1
    except:
        return jsonify("error:threesigma")
    return jsonify(a)