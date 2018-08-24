list = []
list1 = []
n = int(input())
list = [[str(input()),float(input())] for _ in range(n)]
list.sort(key = lambda x : x[1])
a = 0
b = 1
val = list[1][1]
for items in list:
    if val == list[a+1][b]:
        list1.append(list[a+1][0])
        a=a+1
list1.sort()
for item in list1:
    print(item)