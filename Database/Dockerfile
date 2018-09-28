FROM microsoft/mssql-server-linux

ENV SA_PASSWORD=Welcome1234
ENV DATABASE_NAME=HivemindDb
ENV ACCEPT_EULA=Y

COPY . .

RUN chmod +x ./sqlcmdstartup.sh
CMD /bin/bash ./entrypoint.sh
EXPOSE 1433