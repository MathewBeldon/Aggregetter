import { PlaywrightCrawler, Dataset } from 'crawlee';
import { URL } from 'node:url';

const crawler = new PlaywrightCrawler({
    async requestHandler({ request, page, enqueueLinks, log}) {
        const title = await page.title();
        log.info(`Title of ${request.loadedUrl} is '${title}'`);

        await Dataset.pushData({ title, url: request.loadedUrl });
        
        await enqueueLinks({
            selector: '.list-item__title',
            globs: ['https://ria.ru/*/*']
        });
        let link = await page.$('.list-items-loaded');
        let linkAttr = (await link?.getAttribute('data-next-url')) ?? '';
        await crawler.addRequests([new URL(linkAttr, request.loadedUrl).href]);
    },
});

await crawler.run(['https://ria.ru/services/search/getmore/']);