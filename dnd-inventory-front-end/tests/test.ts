import { expect, test } from '@playwright/test';

let joinkey = "";
let sessionName = "testSession";

test.describe.serial('sessions and session page tests', () => {
	test('Session table not to be empty', async ({ page }) => {

		await page.context().storageState({ path: '$/tests/storageState.json' });

		await page.goto('/sessions');
	
		await page.getByRole('button', { name: 'Create new Session'}).click();
		await page.getByLabel('Name:').fill(sessionName);
		await page.getByRole('button', { name: 'Create', exact: true}).click();
		await page.goto('/sessions');

		const table = await page.getByTestId("session-table").last();

		await expect(table).toContainText(sessionName);
	});
	test('Create JoinCode', async ({ page }) => {
		await page.context().storageState({ path: '$/tests/storageState.json' });

		await page.goto('/sessions');

		await page.goto(await page.locator("a").last().getAttribute('href') ?? "");

		await page.getByText(' members and invite code').click();

		await page.getByRole('button', { name: 'create new code'}).click();
		await page.getByLabel('amount of uses:').fill("1");
		await page.getByRole('button', { name: 'Create', exact: true}).click();

		await page.getByText(' members and invite code').click();

		const joinTableEntry = await page.getByTestId("join-key").first();

		await expect(joinTableEntry).toBeVisible();
		
		joinkey = (await joinTableEntry.allInnerTexts()).pop() ?? "";
	});
	test('test', async ({ page }) => {

		page.context().clearCookies();

		await page.goto("/login");
		await page.getByRole('button', { name: 'Log in' }).click();
		await page.getByRole('textbox', { name: 'Email address' }).fill('test2@test.com');
		await page.getByRole('textbox', { name: 'Password' }).fill('end4To2End5Tests');
		await page.getByRole('button', { name: 'Continue', exact: true }).click();

		await page.goto('/sessions');
	
		await page.getByRole('button', { name: 'Join Session'}).click();
		await page.getByLabel('Join Key:').fill(joinkey);
		await page.getByRole('button', { name: 'Join', exact: true}).click();
		await page.goto('/sessions');

		const table = await page.getByTestId("session-table").first();

		await expect(table).toContainText(sessionName);
	});
});