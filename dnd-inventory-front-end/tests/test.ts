import { expect, test } from '@playwright/test';

test.describe.parallel('sessions and session page tests', () => {
	test('Session table not to be empty', async ({ page }) => {
		let sessionName = "testSession";

		await page.context().storageState({ path: '$/tests/storageState.json' });

		await page.goto('/sessions');
	
		await page.getByRole('button', { name: 'Create new Session'}).click();
		await page.getByLabel('Name:').fill(sessionName);
		await page.getByRole('button', { name: 'Create', exact: true}).click();
		await page.goto('/sessions');

		const table = await page.getByTestId("session-table").first();

		await expect(table).toContainText(sessionName);
	});
	test('inventory table not to be empty', async ({ page }) => {
		await page.context().storageState({ path: '$/tests/storageState.json' });
	});
	test('settings tab is not empty', async ({ page }) => {
		await page.context().storageState({ path: '$/tests/storageState.json' });
	});
});