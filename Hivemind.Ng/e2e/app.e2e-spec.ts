import { HivemindngPage } from './app.po';

describe('hivemindng App', function() {
  let page: HivemindngPage;

  beforeEach(() => {
    page = new HivemindngPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
