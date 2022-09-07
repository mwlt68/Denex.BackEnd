FROM mongo

COPY PracticeSchemaInit.json /PracticeSchemaInit.json
CMD mongoimport --host mongodb-service --db DenexDB --collection PracticeSchema --type json --file /PracticeSchemaInit.json --jsonArray
