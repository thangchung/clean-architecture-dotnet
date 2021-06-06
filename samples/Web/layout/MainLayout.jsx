import React from "react";
import { Layout, Row, Col } from "antd";

/* Components */
import NavBar from "./nav/NavBar";
import MyFooter from "./foot/MyFooter";

const { Header, Content, Footer } = Layout;

function MainLayout(mainProps) {
  const { children } = mainProps;

  const props = {};

  return (
    <>
      <Layout>
        <NavBar {...props} />
        <Content
          className="site-layout"
          style={{ padding: "0 50px", marginTop: 100 }}
        >
          <div
            className="site-layout-background"
            style={{ padding: 24, minHeight: 600 }}
          >
            {children}
          </div>
        </Content>
        <MyFooter />
      </Layout>
    </>
  );
}

export default MainLayout;
