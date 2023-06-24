import * as React from 'react';
import { Form, FormInstance, Input, Modal, Select } from 'antd'
import { IUserType } from '../types/UserType.type'
import { ModalType } from '../helper';
import { IUser } from '../types/User.type';

type Props = {
    visible: boolean,
    modalType: ModalType,
    types: IUserType[],
    onCancel: () => void;
    onOk: () => void;
    editUser: IUser | null;
    formRef: React.RefObject<FormInstance>;
}

export default function CreateOrUpdateUser(props: Props) {
    const { visible, onCancel, onOk, modalType, editUser, types, formRef } = props;

    return (
        <Modal
            maskClosable={false}
            open={visible}
            destroyOnClose={true}
            title={modalType === ModalType.CREATE ? 'Создать' : 'Редактировать'}
            onOk={onOk}
            cancelText={"Отмена"}
            onCancel={onCancel}
        >
            <Form
                name="basic"
                ref={formRef}
                initialValues={{ remember: true }}
                autoComplete="off"
            >
                <Form.Item
                    label="Имя"
                    name="name"
                    initialValue={editUser?.name}
                    rules={[{ required: true, message: 'Обязательно для заполнения!' }]}
                >
                    <Input />
                </Form.Item>
                <Form.Item
                    label="Логин"
                    initialValue={editUser?.login}
                    name="login"
                    rules={[{ required: true, message: 'Обязательно для заполнения!' }]}
                >
                    <Input
                    />
                </Form.Item>
                <Form.Item
                    label="Пароль"
                    initialValue={editUser?.password}
                    name="password"
                    rules={[{ required: true, message: 'Обязательно для заполнения!' }]}
                >
                    <Input
                    />
                </Form.Item>
                <Form.Item
                    label="Тип"
                    initialValue={editUser?.typeId !== 0 ? editUser?.typeId : null}
                    name="typeId"
                    rules={[{ required: true, message: 'Обязательно для заполнения!' }]}
                >
                    <Select
                        options={
                            types.map((type: IUserType) => {
                                return {
                                    value: type.id,
                                    label: type.name,
                                }
                            })
                        }
                    />
                </Form.Item>
            </Form>

        </Modal>
    )
}
