import React from "react";
import { Layout } from "antd";

const { Footer } = Layout;

function MyFooter() {
  return (
    <Footer style={{ textAlign: "center" }}>
      Copyright 2021 | Thang Chung. Fork this on&nbsp;
      <a
        href="https://github.com/thangchung/practical-clean-ddd"
        className="text-dark"
      >Github</a>
    </Footer>
  );
}

export default MyFooter;
