import { Button, Collapse, DatePicker, Form, FormInstance, Input, Select } from 'antd';
import React from 'react';
import { IUserType } from '../types/UserType.type';
import dayjs from 'dayjs';
import { useStore } from '../hooks/use-store';
import { observer } from 'mobx-react-lite';
import '../styles/FilterBlock.style.css';

export const FilterBlock = observer(() => {
    const { taskStore } = useStore();

    const formRef = React.createRef<FormInstance>();

    const searchHandle = (values: any) => {
        taskStore.getFilteredUsers({
            name: values.name,
            typeId: values.typeId,
            dateFrom: values.dateFrom.toDate(),
            dateTo: values.dateTo.toDate(),
        });
    }

    const item = (
        <Form
            name="basic"
            ref={formRef}
            initialValues={{ remember: true }}
            onFinish={searchHandle}
            autoComplete="off"
        >
            <Form.Item
                label="Имя пользователя"
                name="name"
                initialValue={''}
            >
                <Input />
            </Form.Item>
            <Form.Item
                label="Тип пользователя"
                name="typeId"
            >
                <Select
                    options={
                        taskStore.userTypes.map((type: IUserType) => {
                            return {
                                value: type.id,
                                label: type.name,
                            }
                        })
                    }
                />
            </Form.Item>
            <Form.Item
                label="Дата с"
                name="dateFrom"
                initialValue={dayjs(new Date(Date.UTC(new Date().getFullYear(), 0, 1)))}
            >
                <DatePicker
                    allowClear={false}
                />
            </Form.Item>
            <Form.Item
                label="Дата по"
                name="dateTo"
                initialValue={dayjs(new Date(Date.UTC(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())))}
            >
                <DatePicker
                    allowClear={false}
                />
            </Form.Item>
            <Form.Item className='submit-button'>
                <Button
                    type="primary"
                    htmlType="submit"
                    disabled={taskStore.isLoading}
                >
                    Поиск
                </Button>
            </Form.Item>
        </Form>
    );

    return (
        <Collapse
            className='filter-block'
            items={[{
                key: '1',
                label: 'Фильтры',
                children: item,
            }]}
        />
    )
});
