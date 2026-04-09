import TableContact from "./layout/TableContact/TableContact";
import { useState, useEffect } from "react";
import FormContact from "./layout/FormContact/FormContact";
import axios from "axios";
import { Route, Routes, useLocation, Link } from "react-router-dom";
import ContactDetails from "./layout/ContactDetails/ContactDetails";
import Pagination from "./layout/Pagination/Pagination";
import AppendContact from "./layout/FormContact/AppendContact";

const baseApiUrl = window.config.apiUrl;

const App = () => {

    const [contacts, setContacts] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [pageSize] = useState(10);
    const location = useLocation();
    const [updateTrigger, setUpdateTrigger] = useState(false);


    const handleUpdateTrigger = () => {
        setUpdateTrigger(updateTrigger + 1);
    }

    const baseUrl = `${baseApiUrl}/contacts`;

    useEffect(() => {
        axios.get(baseUrl + `/page?pageNumber=${currentPage}&pageSize=${pageSize}`).then((response) => {
            setContacts(response.data.contacts);
            setTotalPages(Math.ceil(response.data.totalCount / pageSize));
        })
    }, [currentPage, pageSize, location.pathname, updateTrigger])

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    }

    const addContact = (contactName, phoneNumber, contactEmail) => {

        //Так можно отсортировать по возрастанию и найди максимальный элемент
        // const newId = contacts
        //     .sort((x, y) =>  x.id - y.id)[contacts.length - 1]
        //     .id + 1;
        // let newId;
        // if (contacts.length === 0) {
        //     newId = 1;
        // } else {
        //     newId = Math.max(...contacts.map(e => e.id)) + 1;
        // }

        const item = {
            name: contactName,
            phoneNumber: phoneNumber,
            email: contactEmail,
        }

        axios.post(baseUrl, item);

        axios.get(baseUrl + `/page?pageNumber=${currentPage}&pageSize=${pageSize}`).then(
            (response) => {
                setContacts(response.data.contacts);
                setTotalPages(Math.ceil(response.data.totalCount / pageSize));
            }
        )

    }

    const deleteContact = (id) => {
        setContacts(contacts.filter(item => item.id !== id));
        axios.delete(baseUrl + `/${id}`);
        handleUpdateTrigger();
    }

    return (
        <div className="container mt-5">
            <Routes>
                <Route path="/" element={
                    <div className="card">

                        <div className="card-header">
                            <h1>Список контактов</h1>
                        </div>

                        <div className="card-body">
                            <TableContact
                                contacts={contacts}
                                deleteContact={deleteContact}
                            />

                            <Pagination
                                currentPage={currentPage}
                                totalPages={totalPages}
                                onPageChange={handlePageChange}
                            ></Pagination>
                            <Link to="/append"
                                className="btn btn-success mt-3">
                                Добавить контакт
                            </Link>
                        </div>
                    </div>
                } />
                <Route path="contact/:id" element={<ContactDetails
                    deleteContact={deleteContact}
                    onUpdate={handleUpdateTrigger} />} />
                <Route path="append" element={<AppendContact />} />
            </Routes>
        </div>
    );
}

export default App;
