export class GenerateQueryRes {
    SqlQuery: string;

    constructor(jsonData: any = null) {
        if (!jsonData) { return; }

        this.SqlQuery = jsonData.sqlQuery
    }
}
