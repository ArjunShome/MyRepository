from bs4 import BeautifulSoup as scrap
import requests as r
import pyodbc


# Function to Get all the HREFS
def gethrefs(htmltxt):
    anchors = htmltxt.findAll("a")
    anchorbody = scrap(str(anchors), "html.parser")
    links = list((a['href'] for a in anchorbody.findAll('a', href=True)))
    return links


# Function to get the table columns
def gettablecolumns(link, childlinks):
    link = link.replace("wkhistdata.htm", "")
    # Get All the Child Links
    pagelinks = list(str(link)+str(childlinks[lnk]) for lnk in range(len(childlinks)))
    tblcols = []
    i = 0
    for links in pagelinks:
        if i > 0:
            break
        body = gethtmlbody(links)
        for tr in body.findAll("tr"):
            if i == 3:
                break
            tblcols.append(tr.text)
            i = i + 1
    return tblcols


# Function To get all the Table Names or The Headers
def gettableheader(link, childlinks):
    link = link.replace("wkhistdata.htm", "")
    # Get All the Child Links
    pagelinks = list(str(link) + str(childlinks[lnk]) for lnk in range(len(childlinks)))
    headers = []
    for links in pagelinks:
        body = gethtmlbody(links)
        # getting the Header Texts
        for para in body.findAll("p"):
            for b in para.findAll("b"):
                header = b.text.replace(" ", "").replace(",", "").replace("/", "by")
                headers.append(header)
    return headers


# GET TABLE DATA
def gettabledata(link, childlinks):
    link = link.replace("wkhistdata.htm", "")
    # Get All the Child Links
    pagelinks = list(str(link) + str(childlinks[lnk]) for lnk in range(len(childlinks)))
    tablesdata = []
    for links in pagelinks:
        body = gethtmlbody(links)
        rowsdata = []
        # getting the columns
        for tr in body.findAll("tr")[3:]:
            coldata = []
            for td in tr.findAll("td"):
                coldata.append(td.text)
            rowsdata.append(coldata)
        tablesdata.append(rowsdata)
    return tablesdata


# Insert Records in Database
def insertrecords(dataset, columns, tablenames):
    conn = pyodbc.connect("Driver={SQL Server Native Client 11.0};Server=CESTEST-04\MYSERVER;Database=CES_Practice;Trusted_Connection=Yes;")
    cursor = conn.cursor()
    message = []
    colstr = ""
    # Get the Table Creation String
    for i in range(len(columns)):
        colstr = colstr + columns[i] + " NVARCHAR(100)"
        if i != len(columns)-1:
            colstr = colstr+","
    i = 0
    for data in dataset:
        command = "IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = '"+str(tablenames[i]).replace("-", "_")+"'))BEGIN CREATE TABLE dbo."+str(tablenames[i]).replace("-", "_")+"("+colstr+") END"
        cursor.execute(command)
        cursor.commit()
        for row in data[1:-1]:
            records = {}
            records['table'] = str(tablenames[i]).replace("-", "_")
            records['values'] = tuple(row)
            command = "INSERT INTO {table} VALUES {values};".format(**records)
            cursor.execute(command)
            cursor.commit()
        message.append("SUCCESSFULLY CREATED TABLE " + tablenames[i] + " AND ALL THE RECORDS GOT INSERTED")
        i = i + 1
    return message


# Get The WebPage Body
def gethtmlbody(lnk):
    data = r.get(lnk)
    pgbody = scrap(data.text, "html.parser")
    return pgbody


url = "https://apps.fas.usda.gov/export-sales/wkhistdata.htm"
# GET THE LINK OF All THE WEBPAGES
AllLinks = gethrefs(gethtmlbody(url))
# All TableNames or Headers
AllTables = gettableheader(url, AllLinks)
# All table columns
TableColumns = gettablecolumns(url, AllLinks)
col = col2 = names = columnnames = []
col = str(TableColumns[1]).split(" ")
col2 = str(TableColumns[0]).split(" ")
names = str(TableColumns[2]).split(" ")
nmy = ""
# Remove Space Elements
col = list(i for i in col if len(str(i)) > 1)
col2 = list(j for j in col2 if len(str(j)) > 1)
names = list(k for k in names if len(str(k)) > 0)
for m in col2:
    nmy = nmy+m
    nmy = nmy.replace("-", "").replace(" ", "").replace("|", "")
i = 0
length = len(list(col))
for i in range(len(list(col))):
    if i >= length - 2:
        columnnames.append(nmy+"_"+col[i] + "_" + names[i])
    else:
        columnnames.append(col[i] + "_" + names[i])

# All table data
Tablesdata = gettabledata(url, AllLinks)

# Create Table and Insert Records into Database
messages = insertrecords(Tablesdata, columnnames, AllTables)
for message in messages:
    print(message)
