from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker

from dal.settings import connection

connection_string = '{base}://{user}:{pw}@{host}:{port}/{db}'.format(
    base=connection.BASE,
    user=connection.USERNAME,
    pw=connection.PASSWORD,
    host=connection.HOST,
    port=connection.PORT,
    db=connection.DATABASE
)

DBEngine = create_engine(connection_string)
Session = sessionmaker(bind=DBEngine)

ModelBase = declarative_base()
session = Session()
