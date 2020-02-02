import { TestBed } from '@angular/core/testing';

import { SqlOperationService } from './sql-operation.service';

describe('SqlOperationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SqlOperationService = TestBed.get(SqlOperationService);
    expect(service).toBeTruthy();
  });
});
