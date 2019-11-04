import numpy as np
from keras import models
from sklearn.preprocessing import StandardScaler
from sklearn.externals import joblib 
import os.path

def predict(features):
    currentDir = os.path.dirname(os.path.realpath(__file__))

    features = np.array(features)
    scaler = joblib.load(os.path.join(currentDir, "scaler.save"))
    features = scaler.transform(features)
    model = models.load_model(os.path.join(currentDir, "model.h5"))

    return model.predict_on_batch(features)

