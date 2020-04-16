class RecordDTO:
    def __init__(self, inputs: list, output: float):
        self.inputs = inputs
        self.output = output


class DatasetHeader:
    def __init__(self, _id: str, name: str):
        self.id = _id
        self.name = name


class DatasetOutputs:
    def __init__(self, outputs: list):
        self.outputs = outputs


class DatasetToRead:
    def __init__(self, _id: str, name: str, records: list):
        self.id = _id
        self.name = name
        self.records = records
