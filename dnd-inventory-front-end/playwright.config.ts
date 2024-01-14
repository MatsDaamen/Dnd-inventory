import type { PlaywrightTestConfig } from '@playwright/test';

const config: PlaywrightTestConfig = {
	webServer: {
		command: 'npm run build && npm run preview',
		port: 4173
	},
	use: {
        baseURL: 'http://localhost:4173/',
        storageState: 'tests/storageState.json',
        trace: 'on-first-retry',
        ignoreHTTPSErrors: true,
    },
	testDir: 'tests',
	globalSetup: './tests/global-setup',
	testMatch: /(.+\.)?(test|spec)\.[jt]s/
};

export default config;
