FROM microsoft/mssql-server-linux
ENV SA_PASSWORD=Welcome1234
ENV ACCEPT_EULA=Y
COPY ./entrypoint.sh ./entrypoint.sh
COPY ./sqlcmdstartup.sh ./sqlcmdstartup.sh
COPY ./populate.sql ./populate.sql
COPY ./dbo/ ./dbo/
COPY ./StaticValues/ ./StaticValues/
COPY sqlcmdscript.sql sqlcmdscript.sql
RUN chmod +x ./sqlcmdstartup.sh
CMD /bin/bash ./entrypoint.sh
EXPOSE 1433