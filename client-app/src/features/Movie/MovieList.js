import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from "react-router-dom";
import { Input,Table, Tag, Typography, Button, Row, Col, Modal, Tooltip } from 'antd';
import { EditOutlined, DeleteOutlined,SearchOutlined,RedoOutlined } from '@ant-design/icons';
import WordLayout from '../../components/Layout';
import SeriesService from '../../services/seriesService'





export default function List() {
  const navigate = useNavigate()
  const { Title } = Typography;
  const [APIData, setAPIData] = useState([]);

  async function fetchDeleteMovie(id) {
    var response = await SeriesService.delete(id);
    if (response.success) {
      setAPIData((pre) => {
        return pre.filter((student) => student.id !== id);
      });
    }
  }

  const onDeleteMovie = (id) => {
    Modal.confirm({
      title: "Are you sure, you want to delete this Movie record?",
      okText: "Yes",
      okType: "danger",
      onOk: () => {
        fetchDeleteMovie(id);
      },
    });
  };

  const columns = [
    {
      title: 'Name',
      key: 'name',
      dataIndex: 'name',
      render: (text, record) => <a>  <Link to={`/createMovie/${record.id}`}>
    {text}
    </Link> </a>,
      filterDropdown: ({ setSelectedKeys, selectedKeys, confirm, clearFilters }) => {

        return (
          <>
            <Input
              autoFocus
              value={selectedKeys[0]}
              placeholder="Type text here"
              onPressEnter={() => {
                confirm();

              }}
              onChange={(e) => {

                setSelectedKeys(e.target.value ? [e.target.value] : [])

              }
              }

              onBlur={() => {
                confirm();

              }}>

            </Input>

            <Tooltip title="search">
              <Button style={{ marginLeft: 'auto' }} type="primary" icon={<SearchOutlined />} onClick={() => {
                confirm();
              }} />
            </Tooltip>
            
            <Tooltip title="reset">
              <Button style={{ marginLeft: 'auto' }} type="danger"  icon={<RedoOutlined  />} onClick={() => {
                clearFilters();
              }} />
            </Tooltip>
          </>
        )
      },
      filterIcon: () => {
        return <SearchOutlined />
      },
      filterSearch: true,
      onFilter: (value, record) => record.name.toLowerCase().includes(value.toLowerCase()),
    },
    {
      title: 'Duration',
      dataIndex: 'duration',
    },

    {
        title: 'Seasons',
        dataIndex: 'totalSeason',
      },

    {
      title: 'Tags',
      key: 'tags',
      dataIndex: 'tags',
      render: tags => (
        <>
          {tags.map(tag => {
            let color = tag.length > 5 ? 'geekblue' : 'green';
            if (tag === 'loser') {
              color = 'volcano';
            }
            return (
              <Tag color={color} key={tag}>
                {tag.toUpperCase()}
              </Tag>
            );
          })}
        </>
      ),
    },
    {
      key: "4",
      title: "Actions",
      dataIndex: 'id',
      render: (id) => {
        return (
          <>
            <Link to={`/createMovie/${id}`}>

              <EditOutlined />
            </Link>

            <DeleteOutlined
              onClick={() => {
                onDeleteMovie(id);
              }}
              style={{ color: "red", marginLeft: 12 }}
            />
          </>
        );
      }
    }
  ];

  useEffect(() => {
    async function fetchMyAPI() {
      var response = await SeriesService.getAll();
      console.log(response.data);
      setAPIData(response.data);
    }
    fetchMyAPI()
  }, [])
  return (
    <WordLayout>
      <div>
        <Row gutter={[40, 0]}>
          <Col span={18}>
            <Title level={2}>
              User List
            </Title>
          </Col>
          <Col span={6}>
            <Button onClick={() => navigate('/createMovie/0')} block>Add Movie</Button>
          </Col>
        </Row>
        <Row gutter={[40, 0]}>
          <Col span={24}>
            <Table
              rowKey='id'
              dataSource={APIData}
              columns={columns}
            />
          </Col>
        </Row>
      </div>
    </WordLayout>
  )
}