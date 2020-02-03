export class GenerateQueryRes {
    SqlQuery: string;

    constructor(jsonData: any) {
        if (jsonData != null) {
            this.SqlQuery = jsonData.sqlQuery
        }
    }
}
