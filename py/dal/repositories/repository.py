from sqlalchemy.sql import text

from dal.context import session, DBEngine, ModelBase
from dal.models.dataset import Dataset, Record


class Repository(object):
    def __init__(self, entity_model):
        self.DBEngine = DBEngine
        self.Session = session
        self.ModelBase = ModelBase
        self.ModelBase.metadata.create_all(self.DBEngine)
        self.entity_model = entity_model

    def get_all(self):
        return self.Session.query(self.entity_model).all()


class DatasetRepository(Repository):
    def __init__(self):
        Repository.__init__(self, Dataset)

    def get_dataset_by_id(self, _id: str):
        query = text('SELECT "Id", "Name" FROM "Dataset" WHERE "Id" = :id')
        proxy = self.DBEngine.execute(query, id=_id).first()
        return Dataset(proxy[0], proxy[1])

    def insert_new_dataset(self, dataset: Dataset):
        query = text('''INSERT INTO "Dataset" ("Id", "Name") 
                        VALUES (:id, :name)''')
        self.DBEngine.execute(query, id=dataset.Id, name=dataset.Name)

    def delete_dataset(self, _id: str):
        query = text('''DELETE FROM "Dataset"
                        WHERE "Id" = :id''')
        self.DBEngine.execute(query, id=_id)


class RecordRepository(Repository):
    def __init__(self):
        Repository.__init__(self, Record)

    def get_dataset_records(self, _id: str):
        query = text('''SELECT "Id", "Inputs", "Output", "DatasetId" FROM "Record"
                        WHERE "DatasetId" = :datasetid''')
        return self.DBEngine.execute(query, datasetid=_id)

    def insert_record(self, record: Record):
        query = text('''INSERT INTO "Record" ("Inputs", "Output", "DatasetId")
                        VALUES (:inputs, :output, :datasetid)''')
        self.DBEngine.execute(query, inputs=record.Inputs, output=record.Output, datasetid=record.DatasetId)

    def delete_records_of_dataset(self, _id: str):
        query = text('''DELETE FROM "Record"
                        WHERE "DatasetId" = :datasetid''')
        self.DBEngine.execute(query, datasetid=_id)
