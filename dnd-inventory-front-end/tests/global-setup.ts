import { chromium, type FullConfig } from '@playwright/test';

async function globalSetup(config: FullConfig) {
  const { baseURL, storageState } = config.projects[0].use;
  const browser = await chromium.launch();
  const page = await browser.newPage();
  await page.goto(baseURL + "login");
  await page.getByRole('button', { name: 'Log in' }).click();
  await page.getByRole('textbox', { name: 'Email address' }).fill('test@test.com');
  await page.getByRole('textbox', { name: 'Password' }).fill('end4To2End5Tests');
  await page.getByRole('button', { name: 'Continue', exact: true }).click();
  await page.context().storageState({ path: storageState as string });
  await browser.close();
}

export default globalSetup;