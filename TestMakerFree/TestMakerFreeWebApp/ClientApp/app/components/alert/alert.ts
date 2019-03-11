/**
 * Alert type.
 */
export interface TestMakerAlert {
  text: string;
  type: TestMakertAlertType;
  css?: string;
}

/**
 * Supported alert types.
 */
export enum TestMakertAlertType {
  Error = 1
}