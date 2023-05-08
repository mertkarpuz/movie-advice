
# Movie Advice



Vizyona giren filmlere erişebilir, film bilgilerini detaylı bir şekilde görebilir, yorum yapıp puan verebilirsiniz. Ayrıca mail yolu ile arkadaşlarınıza önerebilirsiniz.


## API Kullanımı

#### Mail yolu ile film önermek için kullanılır. (Authorization gereklidir.)

```http
  POST /advice/makeadvice
```

#### Kullanıcının uygulamaya log-in olması için kullanılır.

```http
  POST /api/auth/login
```


#### Kullanıcının seçilen filme yorum ve puan vermesi için kullanılır. (Authorization gereklidir.)

```http
  POST /api/comment/addcomment
```

#### Kullanıcının seçilen filme yorum ve puan vermesi için kullanılır.

```http
  GET /api/movies/getmovies
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `page`      | `int` |  Sayfa sayısı |


#### Kullanıcının seçilen filmi detaylı görüntüleyebilmesi için kullanılır (yorumlar, puanlar ve kullanıcılar dahildir.)
```http
  POST /api/movies/getmovie
```
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `movieId`      | `int` |  movie identifier |



## Film önerme mail görüntüsü:



  
![img](https://i.imgyukle.com/img/2023/05/08/rkUNdM.png)

    