from bs4 import BeautifulSoup as scrap
import pandas as pd
import requests as r
import json

# GET THE DATA FROM WEBSITE's MAIN BODY
data = r.get("https://www.imdb.com/chart/top?ref_=nv_mv_250")
soup = scrap(data.text,"html.parser")
tablebody = soup.find("tbody",{"class":"lister-list"})
soup_1 = scrap(str(tablebody),"html.parser")

# GET THE NAME OF THE MOVIES AND THE YEAR
all_a = soup_1.find_all("td",{"class":"titleColumn"})
soup_2 = scrap(str(all_a),"html.parser")
all_td = soup_2.find_all("a")
all_td_1 = scrap(str(all_td),"html.parser")
all_span = soup_2.find_all("span",{"class":"secondaryInfo"})
all_span_1 = scrap(str(all_span),"html.parser")

# GET THE RATING OF THE MOVIE IN IMDB
all_rtng = soup_1.find_all("td",{"class":"ratingColumn imdbRating"})
soup_3 = scrap(str(all_rtng),"html.parser")
rtngs = soup_3.find_all("strong")
ratings = scrap(str(rtngs),"html.parser")

liststr = all_td_1.text.split(", ")
listspan = all_span_1.text.split(", ")
listrtng = ratings.text.split(", ")

#GET THE NUMBER OF MOVIES LISTED
lstlength = len(liststr)

# STORE THE MOVIE YEAR AND RATING DETAILS INTO A JSON FILE FORMAT
data = []

for i in range(lstlength-6):
    innerlst = [str(i),liststr[i],listspan[i],listrtng[i]]
    data.append(innerlst)
frame = pd.DataFrame(data)

header = []
element = []
element.append("SLno")
element.append("Movie Name")
element.append("Year")
element.append("Rating")
header.append(element)
framehdr = pd.DataFrame(header)
frames = [framehdr,frame]

finalframe = pd.concat(frames)

for i in range(4):
    finalframe[i] = finalframe[i].str.strip('[')
    finalframe[i] = finalframe[i].str.strip(']')

#Rename the headers
finalframe = finalframe.rename(columns = finalframe.iloc[0])
finalframe = finalframe.drop(finalframe.index[0])

# print(finalframe)

newfile = open(r"C:\Users\cestrainee\Desktop\WebScrapping_IMDB_DataFrame.txt","w")
newfile.writelines(str(finalframe))
newfile.close()