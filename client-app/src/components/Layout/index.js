
import React, { useState } from "react";
import { Layout } from "antd";
import WordSider from "../Sider";
import WordHeader from "../Header";

const { Content, Footer } = Layout;
function WordLayout(props) {
    const [collapsed, setCollapsed] = useState(true);

    const toggle = () => {
        setCollapsed((prevCollapsed)=>{
            return !prevCollapsed;
        });
      };
  return (
    <>
      <Layout style={{ minHeight: "100vh" }}>

          <WordSider collapsed={collapsed}/>
          <Layout className="site-layout">
            <WordHeader toggle={toggle} collapsed={collapsed}/>

        

            <Content
            className="site-layout-background"
            style={{
              margin: '24px 16px',
              padding: 24,
              minHeight: 280,
            }}
          >
          
          {props.children}
          </Content>
          <Footer style={{ textAlign: "center" }}>
            Word ©2022
          </Footer>
          </Layout>
      </Layout>
    </>
  );
}

export default WordLayout;