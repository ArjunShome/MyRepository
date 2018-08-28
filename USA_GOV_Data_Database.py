from bs4 import BeautifulSoup as scrap
import requests as r
import time

print(time.strftime("%b %d %Y %H:%M:%S", time.gmtime()))


# Function to Get all the HREFS
def gethrefs(htmltxt):
    anchors = htmltxt.findAll("a")
    anchorbody = scrap(str(anchors), "html.parser")
    links = list((a['href'] for a in anchorbody.findAll('a', href=True)))
    return links


# Function To get all the Table Names or The Headers
def gettablenames(link, childlinks):
    link = link.replace("wkhistdata.htm", "")
    # Get All the Child Links
    pagelinks = list(str(link)+str(childlinks[lnk]) for lnk in range(len(childlinks)))
    headers = []
    for links in pagelinks:
        data = r.get(links)
        body = scrap(data.text, "html.parser")

        # getting the Header Texts
        para = body.findAll("p")
        paragraph = scrap(str(para), "html.parser")
        Text = paragraph.findAll("b")
        header = scrap(str(Text), "html.parser")
        headers.append(header.text.replace(" ", " _"))

        # getting the data

    return headers


# GET THE DATA FROM WEBSITE's MAIN BODY
url = "https://apps.fas.usda.gov/export-sales/wkhistdata.htm"
data = r.get(url)
body = scrap(data.text, "html.parser")

# All Links
AllLinks = gethrefs(body)
# print(AllLinks)

# All TableNames or Headers
AllTables = gettablenames(url, AllLinks)
print(AllTables)

print(time.strftime("%b %d %Y %H:%M:%S", time.gmtime()))