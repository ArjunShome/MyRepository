from bs4 import BeautifulSoup as scrap
import requests as r

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

# STORE THE MOVIE YEAR AND RATING DETAILS INTO A FILE
newfile = open(r"C:\Users\cestrainee\Desktop\WebScrapping_IMDB_Top_List.txt","w")
for i in range(lstlength-6):
    newfile.write(""+str(i)+"   "+liststr[i]+"   "+listspan[i]+"    "+listrtng[i]+"\n")
newfile.close()

