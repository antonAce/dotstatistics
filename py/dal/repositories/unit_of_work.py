from dal.context import session, ModelBase


class UnitOfWork(object):
    def __init__(self):
        self.Session = session
        self.ModelBase = ModelBase

    def commit(self):
        self.Session.commit()
