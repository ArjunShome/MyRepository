from bs4 import BeautifulSoup as scrap
import requests as r
import pyodbc


# Get The WebPage Body
def gethtmlbody(lnk):
    data = r.get(lnk)
    pgbody = scrap(data.text, "html.parser")
    return pgbody


# Get the Data
def FetchData(html, link):
    link = link.replace("wkhistdata.htm", "")
    message = []
    anchors = html.findAll("a")
    anchorbody = scrap(str(anchors), "html.parser")
    for a in anchorbody.findAll('a', href=True):
        header = ""
        tblcols = []
        tablesdata = []
        parselink = str(link) + str(a['href'])
        linkbody = gethtmlbody(parselink)

        # GET HEADER
        for para in linkbody.findAll("p"):
            for b in para.findAll("b"):
                header = b.text.replace(" ", "").replace(",", "").replace("/", "by")

        # GET COLUMNS
        i = 0
        for tr in linkbody.findAll("tr"):
            if i == 3:
                break
            tblcols.append(tr.text)
            i = i + 1

        col = col2 = names = columnnames = []
        col = str(tblcols[1]).split(" ")
        col2 = str(tblcols[0]).split(" ")
        names = str(tblcols[2]).split(" ")
        nmy = ""
        # Remove Space Elements
        col = list(i for i in col if len(str(i)) > 1)
        col2 = list(j for j in col2 if len(str(j)) > 1)
        names = list(k for k in names if len(str(k)) > 0)
        for m in col2:
            nmy = nmy + m
            nmy = nmy.replace("-", "").replace(" ", "").replace("|", "")
        i = 0
        length = len(list(col))
        for i in range(len(list(col))):
            if i >= length - 2:
                columnnames.append(nmy + "_" + col[i] + "_" + names[i])
            else:
                columnnames.append(col[i] + "_" + names[i])

        colstr = ""
        # Get the Table Creation String
        for i in range(len(columnnames)):
            colstr = colstr + columnnames[i] + " NVARCHAR(100)"
            if i != len(columnnames) - 1:
                colstr = colstr + ","

        # GET DATA
        rowsdata = []
        # getting the columns
        for tr in linkbody.findAll("tr")[3:]:
            coldata = []
            for td in tr.findAll("td"):
                coldata.append(td.text)
            rowsdata.append(coldata)
        tablesdata.append(rowsdata)

        # INSERT IN DB
        conn = pyodbc.connect(
            "Driver={SQL Server Native Client 11.0};Server=CESTEST-04\MYSERVER;Database=CES_Practice;Trusted_Connection=Yes;")
        cursor = conn.cursor()
        for data in tablesdata:
            command = "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '" + str(header).replace("-", "_") + "'))BEGIN CREATE TABLE dbo." + str(header).replace("-", "_") + "(" + colstr + ") END"
            cursor.execute(command)
            cursor.commit()
            for row in data[1:-1]:
                records = {}
                records['table'] = str(header).replace("-", "_")
                records['values'] = tuple(row)
                command = "INSERT INTO {table} VALUES {values};".format(**records)
                cursor.execute(command)
                cursor.commit()
            message.append("SUCCESSFULLY CREATED TABLE " + header + " AND ALL THE RECORDS GOT INSERTED")
            i = i + 1
    return message


url = "https://apps.fas.usda.gov/export-sales/wkhistdata.htm"
messages = FetchData(gethtmlbody(url), url)
for message in messages:
    print(message)
