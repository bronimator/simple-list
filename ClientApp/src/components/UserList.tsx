import React, { useEffect, useState } from 'react';
import { IUser } from '../types/User.type';
import type { ColumnsType } from 'antd/es/table';
import { Button, FormInstance, Modal, Row, Select, Space, Table, Tooltip } from 'antd';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import CreateOrUpdateUser from './CreateOrUpdateUser';
import { ModalType, SourceType } from '../helper';
import { IEntityDto } from '../types/EntityDto.type';
import { useStore } from '../hooks/use-store';
import { observer } from 'mobx-react-lite';
import '../styles/UserList.style.css';

export const UserList = observer(() => {
    const { taskStore } = useStore();

    const formRef = React.createRef<FormInstance>();

    const [modalVisible, setIsModalVisible] = useState(false);
    const [editedUserId, setEditedUserId] = useState(0);

    const confirm = Modal.confirm;

    const handleCancel = () => {
        setIsModalVisible(false);
    }

    const handleOk = () => {
        formRef.current!.validateFields().then(values => {
            if (editedUserId === 0) {
                taskStore.createUser(values);
            } else {
                taskStore.updateUser({ ...values, id: editedUserId });
            }

            setIsModalVisible(false);
            formRef.current!.resetFields();
        }).catch((reject) => {
            console.log(reject);
        });
    }

    const modalOpen = (id: number) => {
        setEditedUserId(id);
        setIsModalVisible(true);

        setTimeout(() => {
            formRef.current?.setFieldsValue({ ...taskStore.editUser });
        }, 100);
    }

    const createOrUpdateModalOpen = async (entityDto: IEntityDto) => {
        if (entityDto.id === 0) {
            taskStore.createNewUser();
            modalOpen(entityDto.id);
        } else {
            taskStore.getUser(entityDto.id)
                .then(() => {
                    modalOpen(entityDto.id);
                });
        }
    }

    const deleteHandler = (id: number) => {
        confirm({
            title: 'Вы действительно хотите удалить пользователя?',
            onOk() {
                taskStore.deleteUser(id);
            },
        });
    }

    const columns: ColumnsType<IUser> = [
        {
            title: 'Имя пользователя',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Логин',
            dataIndex: 'login',
            key: 'login',
        },
        {
            title: 'Пароль',
            dataIndex: 'password',
            key: 'password',
        },
        {
            title: 'Тип пользователя',
            dataIndex: 'typeId',
            key: 'typeId',
            render: (typeId) => {
                const userType = taskStore.userTypes.find(x => x.id === typeId);
                return userType ? userType.name : '';
            }
        },
        {
            title: 'Дата последнего визита',
            dataIndex: 'last_visit_date',
            key: 'last_visit_date',
            render: (last_visit_date: Date | null) => {
                if (last_visit_date) {
                    let date = new Date(last_visit_date);

                    return (
                        <span>{`${date.toLocaleDateString()}`}</span>
                    );
                } else {
                    return (
                        <span></span>
                    )
                }
            }
        },
        {
            title: 'Действия',
            key: 'action',
            render: (_, record) => (
                <Space size="middle">
                    <Tooltip title="Редактировать" >
                        <Button type="dashed" shape="circle" icon={<EditOutlined />} onClick={() => createOrUpdateModalOpen({ id: record.id })} />
                    </Tooltip>
                    <Tooltip title="Удалить" >
                        <Button type="dashed" shape="circle" icon={<DeleteOutlined />} onClick={() => deleteHandler(record.id)} />
                    </Tooltip>
                </Space>
            ),
        },
    ];

    const sourceTypeHandler = (value: SourceType) => {
        taskStore.updateSource(value);
    }

    return (
        <div className='user-list'>
            <Row className='top-panel'>
                <Button
                    type="primary"
                    className='add-button'
                    disabled={taskStore.isLoading}
                    onClick={() => createOrUpdateModalOpen({ id: 0 })}
                >
                    Добавить пользователя
                </Button>
                <div className='source-type-label'>Источник данных:</div>
                <Select
                    className='source-type-select'
                    onChange={sourceTypeHandler}
                    value={taskStore.sourceType}
                    options={[
                        {
                            value: SourceType.DATABASE,
                            label: 'БД'
                        },
                        {
                            value: SourceType.FILE,
                            label: 'Файл'
                        }
                    ]}
                />
            </Row>
            <Table
                columns={columns}
                dataSource={taskStore.users}
                loading={taskStore.isLoading}
                pagination={{ hideOnSinglePage: true }}
            />
            <CreateOrUpdateUser
                visible={modalVisible}
                modalType={editedUserId !== 0 ? ModalType.EDIT : ModalType.CREATE}
                types={taskStore.userTypes}
                onOk={handleOk}
                onCancel={handleCancel}
                editUser={taskStore.editUser}
                formRef={formRef}
            />
        </div>
    )
});
