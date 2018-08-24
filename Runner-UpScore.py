if __name__ == '__main__':
    n = int(input())
    arr = map(int, input().split())
    list = []
    a = 0
    b = 0
    number = 0
    for f in arr:
        list.append(f)
        list.sort()
    while a<n-1 :
        if list[a]>list[a+1] and list[a] < max(list):
            b = list[a]
        elif list[a+1] > list[a] and list[a+1] < max(list) :
            b = list[a+1]
        else : b = list[a]
        if b < max(list):
            number = b
        a+=1
    print(number)
