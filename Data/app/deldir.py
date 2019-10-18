# Auhtor: kyle Reid

import shutil

def obliterate(path):
  '''
  Deletes an entire directory given a complete file path

  :param path: full file path
  :return:
  '''
  
  shutil.rmtree(path)
  
