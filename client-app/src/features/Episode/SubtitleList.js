import React, { useEffect, useState } from 'react';
import { useNavigate, Link, useParams } from "react-router-dom";
import { Input, Table, Tag, Typography, Button, Row, Col, Badge, Statistic, Modal, Tooltip, Divide, message } from 'antd';
import { ArrowUpOutlined, EditOutlined, DeleteOutlined, SearchOutlined, RedoOutlined, CopyOutlined } from '@ant-design/icons';
import { CopyToClipboard } from 'react-copy-to-clipboard';
import WordLayout from '../../components/Layout';
import subtitleService from '../../services/subtitleService'
import SubtitleCreateModal from './SubtitleCreateModal';




export default function SubtitleList() {
  const [messageApi, contextHolder] = message.useMessage();

  const success = () => {
    messageApi.open({
      type: 'success',
      content: 'Text is copied',
    });
  };
  const { id } = useParams();
  async function fetchDeleteSubtitle(id) {
    var response = await subtitleService.delete(id);
    if (response.success) {
      setAPIData((pre) => {
        return pre.filter((word) => word.id !== id);
      });
    }
  }

  const onDeleteSubtitle = (id) => {
    Modal.confirm({
      title: "Are you sure, you want to delete this word record?",
      okText: "Yes",
      okType: "danger",
      onOk: () => {
        fetchDeleteSubtitle(id);
      },
    });
  };

  const columns = [
    {
      title: 'Text',
      key: 'text',
      dataIndex: 'text',
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
              <Button style={{ marginLeft: 'auto' }} type="danger" icon={<RedoOutlined />} onClick={() => {
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
      onFilter: (value, record) => record.text.toLowerCase().includes(value.toLowerCase()),
      render: text => <a>{text}</a>,
    },
    {
      title: 'TurkishText',
      key: 'turkishText',
      dataIndex: 'turkishText',
      filterSearch: true,
      onFilter: (value, record) => record.turkishTranslator.includes(value),
    },

    {
      title: 'Section',
      dataIndex: 'id',
      key: 'id',
      defaultSortOrder: 'ascend',
      sorter: (a, b) => a.id - b.id,
      render: section => (
        <>
          {
            <Badge overflowCount={9999} count={section} />
          }
        </>
      )
    },

    {
      title: 'Start Time - End Time',
      dataIndex: 'startTime',
      key: 'startTime',
      render: (item, row) => (
        <>
          {
            <>
              <Row>
                <Tag color='green'>{item}</Tag>
              </Row>
              <Row>
                <Tag color='red'>{row.endTime}</Tag>
              </Row>
            </>
          }
        </>
      )
    },
    // {
    //   title: 'End Time',
    //   dataIndex: 'endTime',
    //   key: 'endTime',
    //   render: counts => (
    //     <>
    //       {
    //         <Tag color='red'>{counts}</Tag>
    //       }
    //     </>
    //   )
    // },
    {
      title: 'Is Favourite',
      dataIndex: 'isFavourite',
      key: 'isFavourite',
      render: remember => (
        <>
          {
            (remember) ?
              (
                <Tag color="#00cc00">
                  Favourite
                </Tag>
              ) :
              (
                <Tag color="#ff0000">
                  Not Favourite
                </Tag>
              )
          }
        </>
      ),
    },
    {
      title: 'Action',
      dataIndex: 'id',
      key: 'action',
      render: (id, row) => {
        return (
          <>
            {contextHolder}

            <EditOutlined style={{ color: "blue" }} onClick={() => openSubsEditModal(id)} />
            <DeleteOutlined
              style={{ color: "red", marginLeft: 12 }}
              onClick={() => {
                onDeleteSubtitle(id);
              }}

            />


            <CopyToClipboard text={row.text}>
              <CopyOutlined onClick={success} style={{ color: "black", marginLeft: 12 }} />
            </CopyToClipboard>
          </>
        );
      }
    }

  ];
  const { Title } = Typography;
  const navigate = useNavigate()
  const [APIData, setAPIData] = useState([]);
  const [open, setOpen] = useState(false);
  const [selectedSubId, setSelectedSubId] = useState('0');
  useEffect(() => {
    let response;
    async function fetchMyAPI() {
        debugger;
      if (id === undefined || id == 0) {
        response = await subtitleService.get();
      }
      else {
        response = await subtitleService.getByEpisodeId(id);
      }
      console.log(response);
      setAPIData(response.data);
    }
    fetchMyAPI()
  }, [])

  function openSubsEditModal(id) {
    setSelectedSubId(id);
    setOpen(true);
  }
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
            <Button onClick={() => navigate('/create/0')} block>Add User</Button>
          </Col>
        </Row>
        <Row gutter={[40, 0]}>
          <Col span={24}>
            <Table
              rowKey='id'
              dataSource={APIData}
              columns={columns}
              pagination={{ pageSize: 50 }}
              expandable={{
                expanndedRowRender: record => <p style={{ margin: 0 }}>{record.turkishText}</p>,
                rowExpandable: record => record.turkishText !== '',
              }}
            />
          </Col>
        </Row>
      </div>


      {
        selectedSubId != 0 &&
        <SubtitleCreateModal
          open={open}
          onCancel={() => {
            setOpen(false);
          }}
          id={selectedSubId}
        />
      }
    </WordLayout>
  )
}