from sqlalchemy import Column, String, Integer, Float, ForeignKey
from dal.context import ModelBase


class Dataset(ModelBase):
    __tablename__ = 'Dataset'

    Id = Column(String, primary_key=True)
    Name = Column(String, nullable=False)

    def __init__(self, _id, _name):
        self.Id = _id
        self.Name = _name


class Record(ModelBase):
    __tablename__ = 'Record'

    Id = Column(Integer, primary_key=True)
    Inputs = Column(String, nullable=False)
    Output = Column(Float, nullable=False)

    DatasetId = Column(String, ForeignKey('Dataset.Id'))

    def __init__(self, _inputs, _output, _dataset_id):
        self.Inputs = _inputs
        self.Output = _output
        self.DatasetId = _dataset_id
