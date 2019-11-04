import mysql.connector
import numpy as np
from keras import layers
from keras import models
from sklearn.preprocessing import StandardScaler
from sklearn.externals import joblib

def pull_data():
    db = mysql.connector.connect(
        host="localhost",
        user=user,
        password=password,
        database="api"
        )

    cursor = db.cursor(buffered = True)

    cursor.execute("SELECT file.total_inserts, file.insert_averages, " \
               "file.total_deletions, file.deletion_averages, total_lines, " \
               "file.line_averages, file.commit_size, " \
               "repo.assignees, repo.size, repo.commits, repo.events, " \
               "repo.forks, repo.branches, repo.contributors, repo.labels, " \
               "repo.language_count, repo.language_size, repo.milestones, " \
               "repo.issues, repo.refs, repo.stargazers, repo.subscribers, " \
               "repo.watchers, repo.network, repo.open_issues, repo.pulls, " \
               "repo.num_files, repo.commit_size, repo.commit_count," \
               "repo.insertions, repo.deletions, repo.lines_changed " \
               "FROM file INNER JOIN repo ON file.repoID = repo.id")

    data = np.array(cursor.fetchall(), dtype=float)

    cursor.execute("Select file.has_fault FROM file INNER JOIN repo on "\
            "file.repoID = repo.id")

    labels = np.array(cursor.fetchall(), dtype=float)
    cursor.close()
    db.close()

    return data, labels

def shuffle(data, labels):
    
    shuffled = np.random.permutation(len(data))
    data_shuffled = data[shuffled]
    labels_shuffled = labels[shuffled]

    return data_shuffled, labels_shuffled

def hold_out_validation(data, data_labels, percentage):

    num_validation_samples = int(len(data) * (percentage / 100))

    train_data = data[num_validation_samples:]
    train_data_labels = data_labels[num_validation_samples:]

    validation_data = data[:num_validation_samples]
    validation_data_labels = data_labels[:num_validation_samples]

    return (train_data, train_data_labels), (validation_data, validation_data_labels)


# pull data    
data, labels = pull_data()

# shuffle data
data, labels = shuffle(data, labels)

# normalize data
scaler = StandardScaler()
data = scaler.fit_transform(data)
joblib.dump(scaler, "scaler.save") 
    
validation_percentage = 20
(train_data, train_labels), (validation_data, validation_labels) = hold_out_validation(data, labels, validation_percentage)

    
model = models.Sequential()
model.add(layers.Dense(512, activation="relu", input_shape=(data.shape[1],)))
model.add(layers.Dense(512, activation="relu"))
model.add(layers.Dense(1, activation="sigmoid"))

model.summary()

model.compile(optimizer="adam", loss="binary_crossentropy",
        metrics=["accuracy"])

model.fit(train_data, train_labels, validation_data=(validation_data,
            validation_labels), epochs=100)

model.save("model.h5")
