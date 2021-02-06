import React from "react";
import { Container, Row, Col } from "reactstrap";
import styled from "styled-components";

/* Components */
import NavBar from "./nav/NavBar";
import Footer from "./foot/Footer";

const MyRow = styled(Row)`
  margin-top: 30px;
`

function MainLayout(mainProps) {
  const { children } = mainProps;

  const props = {};

  return (
    <>
      <NavBar {...props} />
      <Container fluid>
        <MyRow>
          <Col>{children}</Col>
        </MyRow>
      </Container>
      <Footer />
    </>
  );
}

export default MainLayout;
