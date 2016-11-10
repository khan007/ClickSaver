#ifndef _AODB_INC
#define _AODB_INC


/* Global Constants */
#define AODB_ERR_NOERROR    0
#define AODB_ERR_NODLL      1
#define AODB_ERR_NOINIT     2
#define AODB_ERR_NODB       3
#define AODB_TYP_ITEM       0xf4254
#define AODB_TYP_TEXTURE    0xf6954
#define AODB_TYP_ICON       0xf6958

#define AODB_TYP_PF         0xF4241

/* Functions */
long InitAODatabase( char* strAOFolder );
void ReleaseAODatabase( void );


#endif /* _AODB_INC */
