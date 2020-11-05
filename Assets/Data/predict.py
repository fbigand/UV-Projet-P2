import pickle
import sys, getopt
import numpy as np

#!/usr/bin/python

def main(argv):
  
  pkl_filename = "pickle_model.pkl"
  
  with open(pkl_filename, 'rb') as file:
    pickle_model = pickle.load(file)

  xMin = -1.2
  xMax = 9.6
  yMin = -5.4
  yMax = 5.4

  argv[0] = (argv[0] + 1.2)/10.8
  argv[1] = (argv[1]+5.4)/10.8
  argv[2] = argv[2]/360

  ligne_vide = np.zeros(len(argv))
  input = [argv,ligne_vide]

  output = pickle_model.predict(input)

  print(output[0])

if __name__ == "__main__":
   main(sys.argv[1:])