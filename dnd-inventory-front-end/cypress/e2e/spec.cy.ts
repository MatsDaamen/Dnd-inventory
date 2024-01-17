describe('tests', () => {
  it('Test Authentication and session creation', () => {
    cy.visit('/sessions');
    cy.get('button:contains("Log in")').click();

    cy.origin("https://dev-alag4swf4bg4ii2e.us.auth0.com", () => {
      cy.get('input[id="username"]').type('test@test.com');
      cy.get('input[id="password"]').type('end4To2End5Tests');
      cy.get('button[name="action"]').click();
    });

    cy.visit("/sessions");
    cy.get('button:contains("Create new session")').click();
    cy.get('input:contains("Name:")').type("test session");
    cy.get('button:contains("Create")').click();

    const table = cy.get('list');
  
    expect(table).to.deep.contain("test session");
  })
})