# CryptoInfoApi

## 專案簡介
本專案為 ASP.NET Core 8.0 Web API，具備以下功能：
- 幣別資料表 CRUD API（含中文名稱）
- 呼叫 coindesk API 並轉換資料
- 新 API 提供更新時間、幣別、中文名稱、匯率
- Entity Framework Core 搭配 SQL Server Express LocalDB
- 所有功能皆有單元測試
- Swagger UI
- 可運行於 Docker
- API 與外部 API request/response log
- Error handling
- 多語系設計
- 設計模式應用
- 加解密技術應用（AES/RSA）

## 快速啟動
1. `dotnet restore`
2. `dotnet build`
3. `dotnet run`

## Docker 執行
```
docker build -t cryptoinfoapi .
docker run -p 5000:80 cryptoinfoapi
```

## SQL 建表語法
```
CREATE TABLE [dbo].[Currency] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Code] NVARCHAR(10) NOT NULL,
    [ChineseName] NVARCHAR(50) NOT NULL
);
```

## 單元測試
- 執行 `dotnet test`

## 加分題
- [ ] API request/response log
- [ ] Error handling
- [ ] swagger-ui
- [ ] 多語系設計
- [ ] design pattern 實作
- [ ] Docker 支援
- [ ] 加解密技術應用

## 其他
- 詳細 API 文件請參見 Swagger UI。
