from sqlalchemy.sql import text

from dal.context import session, DBEngine, ModelBase
from dal.models.dataset import Dataset


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
