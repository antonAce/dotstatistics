class Polynomial:
    def __init__(self, koeficients):
        self.koeficients = koeficients


class AccuracyEstimations:
    def __init__(self, approximationOutputs, discreteOutput, squareSumMax, correlation):
        self.approximationOutputs = approximationOutputs
        self.discreteOutput = discreteOutput
        self.squareSumMax = squareSumMax
        self.correlation = correlation