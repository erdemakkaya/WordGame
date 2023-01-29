import React from "react";
import './index.less'
import { Layout, Menu } from "antd";
import {
    FileAddOutlined,
    FormOutlined,
    BarsOutlined,
    TrophyOutlined,
    ClusterOutlined
  } from '@ant-design/icons';
  import {
    Link,
    useLocation    

  } from 'react-router-dom';
const { Sider } = Layout;

function WordSider({collapsed}) {
  const location = useLocation();
  return (
    
    <React.StrictMode>
         <Sider trigger={null} collapsible collapsed={collapsed}>
         <div className="logo"/>
         <Menu theme="dark" mode="inline"  selectedKeys={[location.pathname]}>
            <Menu.Item key="/create/" icon={<FormOutlined />} >
            <Link to="/create/0">
            Create
          </Link>
            </Menu.Item>
            <Menu.Item key="/list" icon={<BarsOutlined />}>
            <Link to="/list">
            Get Words
          </Link>
            
            </Menu.Item>
            <Menu.Item key="3" icon={<TrophyOutlined/>}>
            <Link to="/test">
            Test
          </Link>
            </Menu.Item>

            <Menu.Item key="4" icon={<FileAddOutlined/>}>
            <Link to="/creategrammer/0">
            Create Grammer
          </Link>
            </Menu.Item>
            <Menu.Item key="5" icon={<ClusterOutlined />}>
            <Link to="/listgrammer">
            List  Grammer
          </Link>
            </Menu.Item>
            
          </Menu>
      </Sider>
</React.StrictMode>

  );
}

export default WordSider;