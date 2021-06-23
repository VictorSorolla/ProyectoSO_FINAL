#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <mysql.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
//#include <my_global.h>
#define MAX 100

// -std=c99 `mysql_config --cflags --libs`

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
//
// Estructura para un usuario conectado al servidor.
//
typedef struct 
{
	char nombre [20];
	int socket;
} Conectado;
//
// Estructura de lista de conectados.
//
typedef struct 
{
	Conectado conectados [100];
	int num;
} ListaConectados;



//Variables globales
char ID [3];
ListaConectados miLista;

//
//Funcion que pone en la lista de conectados un usuario
//Anade un nuevo conectado en la lista de conectados o devuelve un -1 si la lista esta llena
//
int Pon (ListaConectados *lista, char nombre[20], int socket)
{
	if(lista->num == 100)
	{
		return -1;
	}
	else
	{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
}
//
//Devuelve el socket del conectado o un -1 si no lo encuentra
//
int DameSocket (ListaConectados *lista, char nombre[20])
{
	int i=0;
	int encontrado = 0;
	while ((i< lista->num) && !encontrado)
	{
		if (strcmp(lista->conectados[i].nombre, nombre) == 0)
			encontrado =1;
		if (!encontrado)
			i=i+1;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
}
//
//Funcion que devuelve la posicion de un usuario pasado por parametro.
//Devuelve la posicion de un usuario en la lista o un -1 si no lo encuentra
//
int DamePosicion (ListaConectados *lista, char nombre [20]) 
{
	int i=0;
	int encontrado = 0;
	while((i<lista->num) && (!encontrado))
	{
		if (strcmp(lista->conectados[i].nombre,nombre) == 0)
		{
			encontrado = 1;
		}
		if (!encontrado)
		{
			i=i+i;
		}
	}
	if(encontrado)
	{
		return i;
	}
	else
	{
		return -1;
	}
}
//
// Funcion que elimina de la lista de conectados el usuario pasado como parametro.
// Devuelve un 0 si se elimina correctamente o un -1 en caso contrario.
//
int Elimina (ListaConectados *lista, char nombre[20])
{
	printf("%s:%d \n", lista->conectados[0].nombre, lista->num);
	printf("Nombre recibido como parametro: %s \n", nombre);
	int pos = DamePosicion(lista, nombre);
	if (pos == -1)
	{
		return -1;
	}
	else
	{
		int i;
		for (i=pos; i < lista->num-1; i++)
		{
			strcpy(lista->conectados[i].nombre, lista->conectados[i+1].nombre);
			lista->conectados[i].socket = lista->conectados[i+1].socket;
		}
		lista->num--;
		printf("Resultado:%d\n", lista->num);
		return 0;
	}
}
//
// Funcion que llena un vector de caracteres con la lista de conectados.
//
void DameConectados (ListaConectados *lista, char conectados[300])
{
	sprintf(conectados,"%d", lista->num);
	int i; 
	for (i=0; i<lista->num; i++)
	{
		sprintf(conectados, "%s/%s", conectados, lista->conectados[i].nombre);
	}
}
//
// Funcion para Loguearse.
// Devuelve un -1 si no se ha encontrado el usuario en la base de datos o un 0 si se logea correctamente
//
int Login (char respuesta[512], char username[20],char password[20],MYSQL *conn)
{
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	strcpy (consulta, "SELECT Jugador.USERNAME FROM (Jugador) WHERE Jugador.USERNAME='");
	strcat (consulta, username); 
	strcat (consulta, "'");
	strcat (consulta, " AND Jugador.PASSWORD='"); 
	strcat (consulta, password); 
	strcat (consulta, "';");
	
	printf("consulta = %s\n", consulta);
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("El USERNAME y el PASSWORD no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("El USERNAME y el PASSWORD no coinciden\n");
		strcpy(respuesta,"El usuario NO ha podido loguearse, revise si el usuario y la contrasena coinciden.");
		return -1;
	}
	
	else
		while (row !=NULL) 
	{
			printf ("Username: %s\n", row[0]);
			row = mysql_fetch_row (resultado);
			return 0;
	}
}
//
//Funcion para dar de baja a un usuario
//Devuelve un -1 si el el usuario o password no coinciden o un 0 si se elimina correctamente
//
int DarDeBaja (char respuesta[200], char username[20],char password[20],MYSQL *conn)
{
	char consulta[200];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	strcpy (consulta, "SELECT Jugador.USERNAME FROM (Jugador) WHERE Jugador.USERNAME='");
	strcat (consulta, username); 
	strcat (consulta, "'");
	strcat (consulta, " AND Jugador.PASSWORD='"); 
	strcat (consulta, password); 
	strcat (consulta, "';");
	
	printf("consulta = %s\n", consulta);
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("El USERNAME y el PASSWORD no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("El USERNAME y el PASSWORD no coinciden\n");
		strcpy(respuesta,"2-El usuario NO existe, revise si el usuario y el password coinciden.");
		return -1;
	}
	
	else
		while (row !=NULL) 
	{
			
			strcpy (consulta, "DELETE FROM Jugador WHERE Jugador.USERNAME='");
			strcat (consulta, username); 
			strcat (consulta, "';");
			
			printf("consulta = %s\n", consulta);
			
			strcpy(respuesta,"2-El usuario ha sido eliminado de la base de datos ");
			
			
			err = mysql_query(conn, consulta);
			if (err!=0)
			{
				printf ("Error al introducir datos la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				strcpy(respuesta,"2-El usuario NO ha sido eliminado de la base de datos ");
				return -1;
				exit (1);
			}
			
			printf("\n");
			printf("Despues de dar baja al jugador deseado la BBDD queda de la siguiente forma:\n");
			err=mysql_query (conn, "SELECT * FROM Jugador");
			if (err!=0) 
			{
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			else
				while (row !=NULL) 
			{
					printf ("Username: %s\n", row[1]);
					row = mysql_fetch_row (resultado);							
			}
				return 0;
	}
}
//
// Funcion que retorna el ID del último jugador registrado en la BBDD.
// Retorna un -1 en caso de que no haya ningun jugador registrado en la BBDD.
//
int DameIDJugador(MYSQL *conn) 
{	
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	//int cont;
	
	strcpy (consulta, "SELECT Jugador.ID FROM Jugador ORDER BY Jugador.ID DESC LIMIT 1 ");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No hay jugadores registrados\n");
		return -1;
	}
	
	else 
	{
		int cont;
		char numero[3];
		while (row !=NULL) 
		{
			printf ("ID del Ãºltimo jugador registrado: %s\n", row[0]);
			cont = atoi(row[0]);
			row = mysql_fetch_row (resultado);
		}
		return cont;
	}
}
//
// Funcion que retorna el ID de la última partida almacenada en la BBDD.
// Retorna un -1 en caso de que no haya ninguna partida registrada en la BBDD.
//
int DameIDPartida(MYSQL *conn) 
{	
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	strcpy (consulta, "SELECT Partida.ID FROM Partida ORDER BY Partida.ID DESC LIMIT 1 ");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No hay ninguna partida\n");
		return -1;
	}
	
	else 
	{
		int cont;
		while (row !=NULL) 
		{
			printf ("ID de la ultima partida: %s\n", row[0]);
			cont = atoi(row[0]);
			row = mysql_fetch_row (resultado);
		}
		return cont;
	}
}
//
// Funcion para dar de alta a un usuario.
// Devuelve un 0 si lo registra correctamente o un -1 si hay algun error
//
int Registrar( char respuesta[200], char username[20],char password[20],MYSQL *conn) 
{
	
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	int IDnum = DameIDJugador(conn);
	IDnum = IDnum + 1;
	
	sprintf(ID, "%d", IDnum);
	strcpy(consulta, "INSERT INTO Jugador VALUES (");
	strcat(consulta,ID);
	strcat(consulta,",'");
	strcat (consulta, username); 
	strcat (consulta, "'");
	strcat (consulta, ",'"); 
	strcat (consulta, password); 
	strcat (consulta, "');");
	
	printf("consulta = %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("El USERNAME y el PASSWORD no coinciden %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	printf("\n");
	printf("Despues de dar alta al jugador deseado la BBDD queda de la siguiente forma:\n");
	err=mysql_query (conn, "SELECT * FROM Jugador");
	if (err!=0) 
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
	}
	else
		while (row !=NULL) 
	{
			printf ("Username: %s\n", row[1]);
			row = mysql_fetch_row (resultado);
			strcpy(respuesta,"3-El usuario se ha REGISTRADO correctamente");
	}
}
//
// Funcion que pone en el vector "fecha" la fecha actual.
// Retorna un -1 en caso de que haya fallado algo.
//
int DameFecha(MYSQL *conn, char fecha[30])
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char consulta[200];
	
	strcpy (consulta, " SELECT CURRENT_DATE;");
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("Algo ha ido mal\n");
		return -1;
	}
	
	else 
	{
		while (row !=NULL) 
		{
			printf ("La fecha es: %s\n", row[0]);
			sprintf(fecha,"%s",row[0]);
			row = mysql_fetch_row (resultado);
		}
		return 0;
	}
}
//
// Funcion que anade en la tabla "Participacion" el ganador de la partida a registar.
// Retorna un -1 en caso de que no se haya podido anadir correctamente.
//
int AnadeParticipacionGanador(MYSQL * conn, char ganador[20], char ID_P [3])
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char ID_J[3];
	char consulta[200];
	
	
	strcpy (consulta, " SELECT Jugador.ID FROM Jugador WHERE Jugador.USERNAME = '");
	strcat (consulta, ganador); 
	strcat (consulta, "';");
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("Algo ha ido mal\n");
		return -1;
	}
	
	else 
	{
		while (row !=NULL) 
		{
			sprintf(ID_J,"%s",row[0]);
			int numeroID = atoi(row[0]);
			sprintf(ID_J,"%d",numeroID);
			
			row = mysql_fetch_row (resultado);
		}
	}
	
	strcpy(consulta, "INSERT INTO Participacion VALUES (");
	strcat(consulta,ID_P);
	strcat(consulta,",");
	strcat (consulta,ID_J);
	strcat (consulta, ");");
	
	printf("consulta añadir participacion ganador = %s\n", consulta);
	
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
}
// 
// Funcion que anade en la tabla "Participacion" el perdedor de la partida a registar.	
// Retorna un -1 en caso de que no se haya podido anadir correctamente.
// 
int AnadeParticipacionPerdedor(MYSQL * conn, char perdedor[20], char ID_P [3])
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char ID_J[3];
	char consulta[200];
	
	
	strcpy (consulta, " SELECT Jugador.ID FROM Jugador WHERE Jugador.USERNAME = '");
	strcat (consulta, perdedor); 
	strcat (consulta, "';");
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("Algo ha ido mal\n");
		return -1;
	}
	
	else 
	{
		while (row !=NULL) 
		{
			sprintf(ID_J,"%s",row[0]);
			int numeroID = atoi(row[0]);
			sprintf(ID_J,"%d",numeroID);
			
			row = mysql_fetch_row (resultado);
		}
		
		
		printf("consulta = %s\n", consulta);
	}
	
	strcpy(consulta, "INSERT INTO Participacion VALUES (");
	strcat(consulta,ID_P);
	strcat(consulta,",");
	strcat (consulta,ID_J);
	strcat (consulta, ");");
	
	printf("consulta añadir participacio perdedor = %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
	printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
	exit (1);
	}
}
//
// Anade a la base de datos, toda la informacion de la partida que se ha jugado
// Devuelve un 0 si la anade correctamente o un -1 si hay algun error
//
int AnadePartidaBD(MYSQL *conn, char ganador[20],char perdedor[20]) 
{
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char fecha[30];
	char consulta[200];
	
	int res = DameFecha(conn, fecha);
	int IDnum = DameIDPartida(conn);
	IDnum = IDnum + 1;
	sprintf(ID, "%d", IDnum);
		
	strcpy(consulta, "INSERT INTO Partida VALUES (");
	strcat(consulta,ID);
	strcat(consulta,",'");
	strcat (consulta, fecha);
	strcat (consulta, "'");
	strcat (consulta, ",'"); 
	strcat (consulta, ganador);
	strcat (consulta, "');");
	
	printf("consulta = %s\n", consulta);
	
	err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha. %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Anadimos los datos de la partida en la tabla de "Participacion"
	AnadeParticipacionGanador(conn,ganador,ID);
	AnadeParticipacionPerdedor(conn,perdedor,ID);
	
	printf("\n");
	printf("Despues de aÃ±adir la ultima partida la BBDD queda de la siguiente forma:\n");
	err=mysql_query (conn, "SELECT * FROM Partida");
	if (err!=0) 
	{
		printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No se han obtenido datos en la consulta\n");
	}
	else
		while (row !=NULL) 
	{
			printf ("ID partida: %s\n", row[0]);
			printf ("Fecha partida: %s\n", row[1]);
			printf ("Ganador partida: %s\n", row[2]);
			row = mysql_fetch_row (resultado);
	}
}
//
// Funcion que pone en el vector "jugadores"(pasado como parametro) los jugadores contra los que he jugado alguna partida.
// Retorna el numero de jugadores contra los que he jugado o un -1 en caso de que no haya jugado contra ningún jugador.
//
int DameUsuariosJugados (char username[20], MYSQL *conn, char jugadores[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT DISTINCT Jugador.USERNAME FROM (Jugador,Partida,Participacion) WHERE Partida.ID IN ");
	strcat (consulta, "( SELECT Partida.ID FROM (Jugador,Partida,Participacion) "); 
	strcat (consulta, "WHERE Jugador.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND Jugador.ID = Participacion.ID_J ");
	strcat (consulta,"AND Participacion.ID_P = Partida.ID) ");
	strcat (consulta,"AND Partida.ID = Participacion.ID_P ");
	strcat (consulta,"AND Participacion.ID_J = Jugador.ID ");
	strcat (consulta, "AND Jugador.USERNAME NOT IN ('");
	strcat (consulta, username); 
	strcat (consulta, "');");
	
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No has jugado ninguna partida con este jugador\n");
		cont = 0;
		return -1;
	}
	
	else 
	{
		cont = 0; 
		char jugadoresjugados[200];
		while (row !=NULL) 
		{
			printf ("Has jugado contra: %s\n", row[0]);
			sprintf(jugadoresjugados, "%s%s/", jugadoresjugados, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		strcpy(jugadores,jugadoresjugados);
		printf("Jugadores con los que has jugado alguna partida : %s\n",jugadores);
		return cont;
	}
}	
//
// Funcion que pone en el vector "ganadores"(pasado como parametro) los ganadores de todas las  
// partidas jugadas contra un determinado jugador("contrincante").
// Retorna el numero de partidos contra ese jugador o un -1 en caso de que no hayan jugado ninguna partida.
//
int DameResultados (char username[20], char contrincante[20], MYSQL *conn, char ganadores[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT Partida.GANADOR FROM (Jugador,Partida,Participacion) WHERE Partida.ID IN ");
	strcat (consulta, "( SELECT Partida.ID FROM (Jugador,Partida,Participacion) "); 
	strcat (consulta, "WHERE Jugador.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND Jugador.ID = Participacion.ID_J ");
	strcat (consulta,"AND Participacion.ID_P = Partida.ID) ");
	strcat (consulta,"AND Partida.ID = Participacion.ID_P ");
	strcat (consulta,"AND Participacion.ID_J = Jugador.ID ");
	strcat (consulta,"AND Jugador.USERNAME = '");
	strcat (consulta, contrincante); 
	strcat (consulta, "' ");
	strcat (consulta,"AND Jugador.ID = Participacion.ID_J ");
	strcat (consulta,"AND Participacion.ID_P = Partida.ID; ");
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		cont = 0;
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("El jugador no ha jugado niguna partida\n");
		return -1;
	}
	
	else 
	{
		cont = 0; 
		char losganadores[200];
		strcpy(losganadores, "");
		while (row !=NULL) 
		{
			printf ("Ganador de la partida: %s\n", row[0]);
			sprintf(losganadores, "%s%s/", losganadores, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		strcpy(ganadores,losganadores);
		printf("EN LA FUNCION SALE :  %s\n", losganadores);
		return cont;
	}
}
//
// Funcion que pone en el vector "respuesta"(pasado como parametro) los jugadores registrados en la BBDD.
// Retorna el numero de jugadores en la BBDD o un -1 en caso de que no hayan jugadores registados.
//
int DameTodosUsuarios ( MYSQL *conn, char respuesta[200], char username[200])
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT * FROM Jugador WHERE Jugador.Username NOT IN ('");
	strcat (consulta, username); 
	strcat (consulta, "');");
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		cont = 0;
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No hay jugadores registrados\n");
		return -1;
	}
	
	else 
	{
		cont = 0; 
		strcpy(respuesta,"");
		while (row !=NULL) 
		{
			printf ("Username:     %s\n", row[1]);
			sprintf(respuesta, "%s%s/", respuesta, row[1]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		return cont;
	}
}	
//
// Funcion que retorna el numero de partidas ganadas del usuario pasado como parámetro.
// Retorna un -1 en caso de no haber encontrado ningún jugador con ese nombre de usuario.
//
int DamePartidasGanadas (char username[20], MYSQL *conn)
{
	char consulta[500];
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int cont;
	
	strcpy (consulta, "SELECT Jugador.USERNAME FROM (Jugador,Partida,Participacion) ");
	strcat (consulta, "WHERE Jugador.USERNAME = '");
	strcat (consulta, username); 
	strcat (consulta, "' ");
	strcat (consulta,"AND Jugador.ID = Participacion.ID_J ");
	strcat (consulta,"AND Participacion.ID_P = Partida.ID ");
	strcat (consulta,"AND Partida.GANADOR ='");
	strcat (consulta, username); 
	strcat (consulta, "';");
	
	
	int err = mysql_query(conn, consulta);
	if (err!=0)
	{
		printf ("Consulta mal hecha %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	resultado = mysql_store_result (conn);
	row = mysql_fetch_row (resultado);
	
	if (row == NULL)
	{
		printf ("No has ganado ninguna partida\n");
		cont = 0;
		return -1;
	}
	
	else 
	{
		cont = 0; 
		char partidasganadas[200];
		strcpy(partidasganadas,"");
		while (row !=NULL) 
		{
			printf ("PARTIDA GANADA: %s\n", row[0]);
			sprintf(partidasganadas, "%s%s/", partidasganadas, row[0]);
			row = mysql_fetch_row (resultado);
			cont= cont +1;
		}
		printf("Numero de partidas ganadas : %d\n",cont);
		return cont;
	}
}	
//
//
// ----------------------------ATENDER CLIENTE ---------------------------------
//
//
//
void *AtenderCliente (void *socket)
{
	int *s;
	s= (int *) socket;
	int sock_conn = * (int *) socket;
	sock_conn = *s;
	
	int i = miLista.num;
	miLista.conectados[i].socket = *s;
	
	//Variables
	char peticion[512];
	char respuesta[512];
	char respuesta2[512];
	int ret;
	MYSQL *conn;
	int err;
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	int numID = 6;
	char consulta [80];
	char notificacion[200];
	char connected[200];
	int socket_invitado;
	int socket_jug1;
	
	
	//Conexion con la base de datos
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	conn = mysql_real_connect (conn, "localhost", "root", "mysql", "bd",0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	//Empieza a escuchar las peticiones del cliente
	int terminar =0;
	while (terminar ==0)
	{		
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		peticion[ret]='\0';
		printf ("Peticion: %s\n",peticion);
		
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		char username[20];
		char password[20];
		//
		// Peticion de DESCONEXION.
		//
		if (codigo ==0)
		{
			pthread_mutex_lock(&mutex);
			int elim = Elimina(&miLista,username);
			pthread_mutex_unlock(&mutex);
			if (elim == 0)
				printf("Usuario eliminado de la lista de conectados\n");
			else	
				printf("Error al eliminar el usuario de la lista de conectados\n");
			
			DameConectados(&miLista,connected);
			
			sprintf(notificacion, "1-%s", connected);
			for (int i=0; i<miLista.num ; i++)
			{
				write (miLista.conectados[i].socket, notificacion, strlen(notificacion));
			}
			printf("Estos son los usuarios conectados actualmente: %s\n", connected);
			
			terminar=1;
			
		}
		//
		// Peticion de LOGUEAR.
		//
		else if (codigo ==1) 
		{
			p = strtok( NULL, "/");
			strcpy (username, p);
			
			p = strtok( NULL, "/");
			strcpy (password, p);
			
			int result = Login(respuesta,username,password, conn );
			if (result == 0)
			{
				
				pthread_mutex_lock(&mutex);
				
				int res = Pon(&miLista, username, sock_conn);
				pthread_mutex_unlock( &mutex);
				printf("%s",username);
				if (res == 0)
					printf("Anadido a la lista de conectados\n");
				if (res != 0)
					printf("Lista llena. No se pudo anadir el usuario a la lista de conectados.\n");
				DameConectados(&miLista,connected);
				printf("Estos son los usuarios conectados actualmente: %s\n", connected);
				
				sprintf(notificacion, "1-%s", connected);
				for (int i=0; i<miLista.num ; i++)
				{
					write (miLista.conectados[i].socket, notificacion, strlen(notificacion));
				}
			}
			else
			{
				printf("El usuario NO ha podido loguearse, revise si el usuario y la contrasena coinciden.");
				strcpy(respuesta,"1-11");
				write(sock_conn,respuesta,strlen(respuesta));
			}
		}
		//
		// Peticion de ELIMINAR USUARIO.
		//
		else if (codigo == 2)
		{
			char DeleteUser[20];
			
			p = strtok( NULL, "/");
			strcpy (DeleteUser, p);
			
			p = strtok( NULL, "/");
			strcpy (password, p);
			
			
			int result = DarDeBaja(respuesta,DeleteUser,password,conn);
			if(result == 0)
			{
				write (sock_conn,respuesta, strlen(respuesta));
			}
			else
			   write (sock_conn,respuesta, strlen(respuesta));
		}
		//
		// Peticion de REGISTRAR.
		//
		else if (codigo ==3) 
		{
			p = strtok( NULL, "/");
			strcpy (username, p);
			
			p = strtok( NULL, "/");
			strcpy (password, p);
			
			Registrar(respuesta, username, password,conn);
			
		}
		//
		//Peticion para INVITAR 
		//
		else if (codigo ==4) 
		{
			
			char invitado [20];
			p=strtok(NULL, "/");
			strcpy(invitado,p);
			
			p=strtok(NULL, "/");
			int nform = atoi(p);
			
			socket_invitado = DameSocket(&miLista,invitado);
			sprintf(respuesta,"4-%s/%d", username, nform);
			write(socket_invitado, respuesta, strlen(respuesta));
			
		}
		//
		//Peticion para RECIBIR INVITACION
		//
		else if (codigo ==5)
		{
			char UserQInvito [20];
			p=strtok(NULL, "/");
			strcpy(UserQInvito,p);
			
			int acepta;
			p=strtok(NULL, "/");
			acepta = atoi(p);
			printf("%d",acepta);
			
			
			if (acepta == 0)
			{
				printf("Jugdor invitado es: %s, El que invita: %s \n",username, UserQInvito);
				socket_invitado = DameSocket(&miLista,UserQInvito);
				sprintf(respuesta,"5-%s/0", username);
				write(socket_invitado, respuesta, strlen(respuesta));
				
				sprintf(respuesta2,"7-%s/%s", UserQInvito, username);
				write(socket_invitado, respuesta2, strlen(respuesta2));
				socket_jug1 = DameSocket(&miLista,username);
				write(socket_jug1, respuesta2, strlen(respuesta2) );
				
			}
			else
			{
				socket_invitado = DameSocket(&miLista,UserQInvito);
				sprintf(respuesta,"5-%s/1", username);
				write(socket_invitado, respuesta, strlen(respuesta));
			}
		}		
		//
		//Peticion para el CHAT
		//
		else if (codigo == 6)
		{
			int sock;
			char usuario[20];
			
			char mensaje[200];
			p=strtok(NULL, "/");
			strcpy(mensaje,p);
			
			sprintf(respuesta, "6-%s/%s", username, mensaje);
			
			
			for (int i=0; i<miLista.num ; i++)
			{
				write (miLista.conectados[i].socket, respuesta , strlen(respuesta));
			}
		}
		//
		//Peticion para reibir y enviar las TIRADAS
		//
		else if (codigo == 7)
		{
			int player; // 1:player1 or 2:player2
			int fila, columna, nForm;
			char jugador1[20];
			char jugador2[20];
			
			printf("%d \n", socket_invitado);
			
			p = strtok( NULL, "/");
			nForm = atoi(p);
			
			p = strtok( NULL, "/");
			player = atoi(p);
			
			p = strtok( NULL, "/");
			fila = atoi(p);
			
			p = strtok( NULL, "/");
			columna = atoi(p);
			
			p=strtok(NULL, "/");
			strcpy(jugador1,p);
			
			p=strtok(NULL, "/");
			strcpy(jugador2,p);
			
			
			
			if(player==1) //jugador 1 envia casilla a jugador 2 
			{	
				sprintf(respuesta, "8-%d/2/%d/%d", nForm, fila , columna);
				int socket_jug2= DameSocket(&miLista, jugador2);
				write(socket_jug2, respuesta, strlen(respuesta) );
				printf("Socket del jugador 2: %d \n", socket_jug2);
				printf("Envio al jugador 2 la fila: %d, y la columna %d \n", fila, columna);
			}
			
			else if(player==2) //jugador 2 envia casilla a jugador 1
			{
				
				sprintf(respuesta, "8-%d/1/%d/%d", nForm, fila , columna);
				int socket_jug1= DameSocket(&miLista, jugador1);
				write(socket_jug1, respuesta, strlen(respuesta) );
				printf("Socket del jugador 1: %d \n",  socket_jug1);
				printf("Envio al jugador 1 la fila: %d, y la columna %d \n", fila, columna);
			}
			
		}
		//
		//Peticion para chequear el GANADOR de la partida.
		//
		else if(codigo ==8)
		{
			int player; // 1:player1 o 2:player2
			char ganador[20];
			char perdedor[20];
			char fecha[20];
			
			p = strtok( NULL, "/");
			strcpy(ganador,p);
			
			p = strtok( NULL, "/");
			strcpy(perdedor,p);
			
			p = strtok( NULL, "/");
			int nform_ganador = atoi(p);
			
			p = strtok( NULL, "/");
			strcpy(fecha,p);
			
			AnadePartidaBD(conn,ganador,perdedor);
			
			int socket_ganador = DameSocket(&miLista, ganador);
			int socket_perdedor = DameSocket(&miLista, perdedor);
			
			
			sprintf(respuesta, "9-%s/%d", ganador, nform_ganador);
			printf("%s \n", respuesta);
			write(socket_ganador, respuesta, strlen(respuesta) );
			
			write(socket_perdedor, respuesta, strlen(respuesta) );
			
			printf("Ha ganado el %s. Hoy es: %s  \n" , ganador, fecha);
		}
		//
		// Peticion para la consulta de jugadores con los que he jugado. 
		//
		else if(codigo == 9)
		{
			char JugadoresJugados[200];
			char jugadores[200];
			int ans = DameUsuariosJugados(username,conn,JugadoresJugados);
			if (ans != -1)
			{
				sprintf(jugadores,"10-%d/%s", ans,JugadoresJugados);
				printf("%s\n", jugadores);
				write(sock_conn, jugadores , strlen(jugadores));
			}
			else
			{
				sprintf(respuesta,"10-0/%s", respuesta);
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		//
		// Peticion para poner en una matriz todos los usuarios registrados en la BBDD.
		//
		else if(codigo == 10)
		{
			char Usuarios[200];
			int resultado = DameTodosUsuarios(conn, Usuarios,username);
			
			if (resultado != -1)
			{
				sprintf(respuesta,"11-%d/%s", resultado, Usuarios);
				printf("%s\n", respuesta);
				write(sock_conn, respuesta , strlen(respuesta));
			}
			else
			{
				sprintf(respuesta,"11-%s", respuesta);
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		//
		// Petcion para enviar al cliente los resultados de las partidas jugadas contra un determinado jugador.
		//
		else if(codigo == 11)
		{
			char ganadoress[200];
			char ganadorescliente[200];
			char contrincante[20]; //Jugador del que quiero ver resultados de las partidas jugadas conta mi.
			
			p = strtok( NULL, "/");
			strcpy(contrincante,p);
			
			int cont = DameResultados(username,contrincante,conn,ganadoress);
			if (cont != -1)
			{
				sprintf(ganadorescliente,"12-%d/%s", cont, ganadoress);
				printf("Esto lo enviamos al cliente %s\n", ganadorescliente);
				write(sock_conn, ganadorescliente , strlen(ganadorescliente));
			}
			else
			{
				sprintf(respuesta,"12-0/%s", respuesta);
				write(sock_conn, respuesta, strlen(respuesta));
			}
		}
		//
		// Peticion para retornar al cliente el numero de partidas ganadas de un jugador.
		//
		else if(codigo == 12)
		{
			char jugador[20]; // jugador del que quiero ver el numero las partidas ganadas.
			strcpy(jugador,"");
			
			p = strtok( NULL, "/");
			strcpy(jugador,p);
			
			int PartidasGanadas = DamePartidasGanadas(jugador,conn);
			sprintf(respuesta,"13-%d",PartidasGanadas);
			write(sock_conn, respuesta , strlen(respuesta));
		}
	}
	close(sock_conn); 
	
}
//
// Aqui empieza el MAIN.
//
int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	serv_adr.sin_port = htons(9300);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	int sockets[100];
	
	pthread_t thread[10];
	ListaConectados miLista;
	i=0;
	
	for (;;)
	{
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		miLista.conectados[i].socket = sock_conn;
		miLista.num = i;
		
		pthread_create (&thread[i], NULL, AtenderCliente,&sockets[i]);
		i++;
	}
}
