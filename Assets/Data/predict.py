import pickle
import sys, getopt
import numpy as np

#!/usr/bin/python

def main(argv):

  #print("1")
  pkl_filename = "C:\\Users\\emile\\Documents\\unity\\UV-Projet-P2\\Assets\\Data\\pickle_model.pkl"
  
  with open(pkl_filename, 'rb') as file:
    pickle_model = pickle.load(file)

  xMin = -1.2
  xMax = 9.6
  yMin = -5.4
  yMax = 5.4

  array_of_string = argv[0].split(";")
  string_to_predict = np.asarray(array_of_string)
  data_to_predict = string_to_predict.astype(np.float)

  data_to_predict[0] = (data_to_predict[0] + 1.2)/10.8
  data_to_predict[1] = (data_to_predict[1]+5.4)/10.8
  data_to_predict[2] = data_to_predict[2]/360

  ligne_vide = np.zeros(len(data_to_predict))
  input = [data_to_predict,ligne_vide]

  output = pickle_model.predict(input)

  print(output[0])



if __name__ == "__main__":
   main(sys.argv[1:])