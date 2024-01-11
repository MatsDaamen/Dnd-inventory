import { expect, test } from '@playwright/test';



test.describe.parallel('sessions and session page tests', () => {
	test('Session table not to be empty', async ({ page }) => {
		await page.goto('/sessions');
	
		const table = await page.innerText('#session-table');
		expect(table).not.toBe('');
	});
	test('inventory table not to be empty', async ({ page }) => {
		await page.goto('/sessions/3');
	
		const table = await page.innerText('#session-table');
		expect(table).not.toBe('');
	});
	test('settings tab is not empty', async ({ page }) => {
		await page.goto('/sessions/3');
	
		const tab = await page.innerText('#settings-tab');
		expect(tab).not.toBe('');
	});
});