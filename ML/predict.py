import numpy as np
from keras import models
from sklearn.preprocessing import StandardScaler
from sklearn.externals import joblib 

def predict(features):

    features = np.array(features)
    scaler = joblib.load("scaler.save")
    features = scaler.transform(features)
    model = models.load_model("model.h5")

    return model.predict_on_batch(features)

